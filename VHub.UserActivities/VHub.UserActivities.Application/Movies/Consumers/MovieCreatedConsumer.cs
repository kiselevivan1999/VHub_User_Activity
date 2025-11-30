using KafkaFlow;
using KafkaFlow.TypedHandler;
using Mapster;
using Microsoft.Extensions.Logging;
using VHub.Media.Api.Contracts.Movies.Events;
using VHub.UserActivities.Application.FavoriteOptions.Handlers;
using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.Movies.Consumers;

public class MovieCreatedConsumer(IFavoriteOptionsHandler favoriteOptionsHandler, ILogger<MovieCreatedConsumer> logger)
    : IMessageHandler<MovieCreatedEvent>
{
    private readonly IFavoriteOptionsHandler _favoriteOptionsHandler =
        favoriteOptionsHandler ?? throw new ArgumentNullException(nameof(favoriteOptionsHandler));

    private readonly ILogger<MovieCreatedConsumer> _logger = logger;

    public async Task Handle(IMessageContext context, MovieCreatedEvent message)
    {
        Console.WriteLine($"🎯 [FORCED LOG] Consumer started for movie: {message}");
        _logger.LogInformation("Обработка консьюмера MovieCreatedConsumer...");

        var userIds = await _favoriteOptionsHandler.GetUserIdsByFavoriteOptionsAsync(
            message.Genres.Adapt<GenreType[]>(), message.PersonIds, CancellationToken.None);

        _logger.LogInformation("Найдены следующие пользователи для оповещения: {userIds}", userIds);
        
        foreach (var item in userIds)
        {
            Console.WriteLine(item);
        }
        
        // todo Заменить на вызов сервиса Vhub.Identity для получения информации о пользователях.
        _logger.LogInformation("Запись в таблицу Reviews.");

        await _favoriteOptionsHandler.WriteNotifyMessage(userIds, message.MovieTitle);
        
        // Отправка события об уведомлении пользователей в сервис VHub.Notifications.
    }
}