using System.Text.Json;
using Confluent.Kafka;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VHub.Media.Api.Contracts.Movies.Events;
using VHub.UserActivities.Application.Contracts.Events;
using VHub.UserActivities.Application.Contracts.Users;
using VHub.UserActivities.Application.FavoriteOptions.Handlers;
using VHub.UserActivities.Application.Movies.Producers;
using VHub.UserActivities.Common.Enums;
using WebApi.Contracts;

namespace VHub.UserActivities.Application.Movies.Consumers;

public class MovieCreatedConsumer : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<MovieCreatedConsumer> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserController _userController;
    private readonly IUsersNotificationRequestedProducer _usersNotificationRequestedProducer;

    public MovieCreatedConsumer(
        IConfiguration config,
        ILogger<MovieCreatedConsumer> logger,
        IServiceProvider serviceProvider,
        IUserController userController,
        IUsersNotificationRequestedProducer usersNotificationRequestedProducer)
    {
        _configuration = config;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _userController = userController;
        _usersNotificationRequestedProducer = usersNotificationRequestedProducer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Старт обработки");
        
        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration["Kafka:BootstrapServers"],
            GroupId = _configuration["Kafka:ConsumerGroupId"] ?? "movie-default-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            EnableAutoOffsetStore = false,
            EnablePartitionEof = true,
            AllowAutoCreateTopics = true,
        };
        
        using var consumer = new ConsumerBuilder<Ignore, string>(config)
            .SetErrorHandler((_, error) => 
                _logger.LogError("Kafka error: {Reason}", error.Reason))
            .SetPartitionsAssignedHandler((_, partitions) =>
            {
                _logger.LogInformation("Assigned partitions: {Partitions}", 
                    string.Join(", ", partitions));
            })
            .Build();
        
        consumer.Subscribe("movie-created");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(stoppingToken);
                
                if (result is { IsPartitionEOF: false })
                {
                    await ProcessMessageAsync(result.Message.Value);
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
        var movieEvent = JsonSerializer.Deserialize<MovieCreatedEvent>(json);
            
        if (movieEvent == null)
        {
            _logger.LogError("Ошибка десерилизации сообщения {EventName}: {Json}", nameof(MovieCreatedEvent), json);
            return;
        }
            
        _logger.LogInformation("Обработка консьюмера MovieCreatedConsumer для фильма {MovieTitle}.", movieEvent.MovieTitle);

        using var scope = _serviceProvider.CreateScope();
                
        var scopedFavoriteOptionsHandler = scope.ServiceProvider
            .GetRequiredService<IFavoriteOptionsHandler>();
        
        var userIds = await scopedFavoriteOptionsHandler.GetUserIdsByFavoriteOptionsAsync(
            movieEvent.Genres.Adapt<GenreType[]>(), movieEvent.PersonIds, CancellationToken.None);

        var users = await _userController.GetByUserIds(userIds, CancellationToken.None);
        
        var usersNotificationRequestedEvent = new UsersNotificationRequestedEvent()
        {
            Users = users.Adapt<UserBriefDto[]>(),
            MovieTitle = movieEvent.MovieTitle,
        };
        await _usersNotificationRequestedProducer.SendUsersNotificationRequestedEvent(
            usersNotificationRequestedEvent, CancellationToken.None);
    }
}