using System.Text.Json;
using Confluent.Kafka;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VHub.Media.Api.Contracts.Movies.Events;
using VHub.UserActivities.Application.FavoriteOptions.Handlers;
using VHub.UserActivities.Common.Enums;
using WebApi.Contracts;

namespace VHub.UserActivities.Application.Movies.Consumers;

public class MovieCreatedConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<MovieCreatedConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly WebApi.Contracts.IUserController _userController;

    public MovieCreatedConsumer(
        IConfiguration config,
        ILogger<MovieCreatedConsumer> logger,
        IServiceProvider serviceProvider,
        IUserController userController)
    {
        _configuration = config;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _userController = userController;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("🚀 Starting Kafka consumer...");
        
        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"],
            GroupId = _configuration["Kafka:ConsumerGroupId"] ?? "movie-default-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            EnableAutoOffsetStore = false,
            EnablePartitionEof = true,
            AllowAutoCreateTopics = true
        };
        
        using var consumer = new ConsumerBuilder<Ignore, string>(config)
            .SetErrorHandler((_, error) => 
                _logger.LogError("Kafka error: {Reason}", error.Reason))
            .SetPartitionsAssignedHandler((_, partitions) =>
            {
                _logger.LogInformation("✅ Assigned partitions: {Partitions}", 
                    string.Join(", ", partitions));
            })
            .Build();
        
        consumer.Subscribe("movie-created");
        _logger.LogInformation("✅ Subscribed to 'movie-created'");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                
                if (result != null && !result.IsPartitionEOF)
                {
                    _logger.LogDebug("📨 Received (Offset: {Offset}, Partition: {Partition})", 
                        result.Offset, result.Partition);
                    
                    await ProcessMessageAsync(result.Message.Value);
                    
                    // Фиксируем offset
                    consumer.Commit(result);
                }
            }
            catch (ConsumeException e)
            {
                _logger.LogError(e, "Consume error");
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Consumer stopped");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Processing error");
                await Task.Delay(1000, stoppingToken);
            }
        }
        
        consumer.Close();
    }

    private async Task ProcessMessageAsync(string json)
    {
// Десериализуем с теми же настройками, что и KafkaFlow (camelCase)
        var movieEvent = JsonSerializer.Deserialize<MovieCreatedEvent>(
            json);
            
        if (movieEvent == null)
        {
            _logger.LogError("Failed to deserialize message: {Json}", json);
            return;
        }
            
        _logger.LogInformation("🎬 Processing: {MovieTitle}", movieEvent.MovieTitle);
            
        Console.WriteLine($"🎯 [FORCED LOG] Consumer started for movie: {json}");
        _logger.LogInformation("Обработка консьюмера MovieCreatedConsumer...");

        using var scope = _serviceProvider.CreateScope();
                
        // Получаем зависимости из scope
        var scopedFavoriteOptionsHandler = scope.ServiceProvider
            .GetRequiredService<IFavoriteOptionsHandler>();
        
        var userIds = await scopedFavoriteOptionsHandler.GetUserIdsByFavoriteOptionsAsync(
            movieEvent.Genres.Adapt<GenreType[]>(), movieEvent.PersonIds, CancellationToken.None);

        var users = (await _userController.GetByUserIds(userIds, CancellationToken.None))
            .Select(x => x.UserName)
            .ToArray();

        _logger.LogInformation("Найдены следующие пользователи для оповещения: {users}", users);
        
        _logger.LogInformation("Запись в таблицу Reviews.");
        await scopedFavoriteOptionsHandler.WriteNotifyMessage(users, movieEvent.MovieTitle);
        
        // Отправка события об уведомлении пользователей в сервис VHub.Notifications.
    }
}