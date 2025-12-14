using VHub.UserActivities.Application.Contracts.Events;

namespace VHub.UserActivities.Application.Movies.Producers;

/// <summary>
/// Продюсер события уведомления пользователей.
/// </summary>
public interface IUsersNotificationRequestedProducer
{
    /// <summary>
    /// Отправляет событие об уведомлении пользователей.
    /// </summary>
    /// <param name="usersNotificationRequestedEvent">Событие об уведомлении пользователей</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    Task SendUsersNotificationRequestedEvent(
        UsersNotificationRequestedEvent usersNotificationRequestedEvent, CancellationToken cancellationToken);
}