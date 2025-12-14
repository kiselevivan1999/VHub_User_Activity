using VHub.UserActivities.Application.Contracts.Users;

namespace VHub.UserActivities.Application.Contracts.Events;

/// <summary>
/// Событие об уведомлении пользователей.
/// </summary>
public class UsersNotificationRequestedEvent
{
    /// <summary>
    /// Пользователи для уведомления.
    /// </summary>
    public UserBriefDto[] Users { get; set; }

    /// <summary>
    /// Название фильма.
    /// </summary>
    public string MovieTitle { get; set; }
}