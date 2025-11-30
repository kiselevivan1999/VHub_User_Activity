using System.Configuration;
using KafkaFlow;
using KafkaFlow.Serializer;
using KafkaFlow.TypedHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VHub.Media.Api.Contracts.Movies.Events;
using VHub.UserActivities.Application.Catalogs.Handlers;
using VHub.UserActivities.Application.Catalogs.Repositories;
using VHub.UserActivities.Application.FavoriteOptions.Handlers;
using VHub.UserActivities.Application.FavoriteOptions.Repositories;
using VHub.UserActivities.Application.MovieRates.Handlers;
using VHub.UserActivities.Application.MovieRates.Repositories;
using VHub.UserActivities.Application.Movies.Consumers;
using VHub.UserActivities.Application.Reviews.Handlers;
using VHub.UserActivities.Application.Reviews.Repositories;

namespace VHub.UserActivities.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCatalogsAppServices()
            .AddMovieRatesAppServices()
            .AddReviewsAppServices()
            .AddFavoriteOptionsAppServices()
            .AddKafkaCluster(configuration);

    private static IServiceCollection AddCatalogsAppServices(this IServiceCollection services)
        => services
            .AddScoped<ICatalogsRepository, CatalogsRepository>()
            .AddScoped<ICatalogsHandler, CatalogsHandler>();
    
    private static IServiceCollection AddMovieRatesAppServices(this IServiceCollection services)
        => services
            .AddScoped<IMovieRatesRepository, MovieRatesRepository>()
            .AddScoped<IMovieRatesHandler, MovieRatesHandler>();

    private static IServiceCollection AddReviewsAppServices(this IServiceCollection services)
        => services
            .AddScoped<IReviewsRepository, ReviewsRepository>()
            .AddScoped<IReviewsHandler, ReviewsHandler>();
    
    private static IServiceCollection AddFavoriteOptionsAppServices(this IServiceCollection services)
        => services
            .AddScoped<IFavoriteOptionsRepository, FavoriteOptionsRepository>()
            .AddScoped<IFavoriteOptionsHandler, FavoriteOptionsHandler>();
    
    public static IServiceCollection AddKafkaCluster(this IServiceCollection services, IConfiguration configuration)
    {
        var kafkaOptions = configuration.GetSection("Kafka");
        var kafkaServers = kafkaOptions["BootstrapServers"];
        var consumerGroup = kafkaOptions["GroupId"] ?? "user-activities-group";

        if (kafkaOptions == null)
        {
            throw new ConfigurationErrorsException($"Конфигурация для Kafka не найдена.");
        }

        if (kafkaServers == null || string.IsNullOrWhiteSpace(consumerGroup))
        {
            throw new ConfigurationErrorsException($"{nameof(consumerGroup)} или {nameof(kafkaServers)} было null.");
        }

        var servers = kafkaServers.Split(',');

        return services
            .AddKafka(kafka => kafka
                .UseMicrosoftLog()
                .AddCluster(cluster => cluster
                    .WithBrokers(servers)
                    .AddConsumer(consumer => consumer
                        .Topic("movie-created")
                        .WithGroupId(consumerGroup)
                        .WithBufferSize(10)
                        .WithWorkersCount(1) // Количество параллельных обработчиков
                        .WithAutoOffsetReset(AutoOffsetReset.Latest)
                        .AddMiddlewares(middlewares => middlewares
                            .AddSerializer<JsonCoreSerializer>()
                            .AddTypedHandlers(handlers => handlers
                                .WithHandlerLifetime(InstanceLifetime.Scoped)
                                .AddHandler<MovieCreatedConsumer>()
                                .WhenNoHandlerFound(context =>
                                {
                                    Console.WriteLine($"Message not handled > Partition: {0} | Offset: {1}",
                                        context.ConsumerContext.Partition,
                                        context.ConsumerContext.Offset);
                                })
                            ))
                    )
                )
            )
            // Регистрация сервисов
            .AddScoped<IMessageHandler<MovieCreatedEvent>, MovieCreatedConsumer>();
    }
}