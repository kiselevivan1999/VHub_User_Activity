using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VHub.UserActivities.Application.Catalogs.Handlers;
using VHub.UserActivities.Application.Catalogs.Repositories;
using VHub.UserActivities.Application.FavoriteOptions.Handlers;
using VHub.UserActivities.Application.FavoriteOptions.Repositories;
using VHub.UserActivities.Application.MovieRates.Handlers;
using VHub.UserActivities.Application.MovieRates.Repositories;
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
            .AddFavoriteOptionsAppServices();
    //.AddKafkaCluster(configuration);

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

        return services;

    }
}