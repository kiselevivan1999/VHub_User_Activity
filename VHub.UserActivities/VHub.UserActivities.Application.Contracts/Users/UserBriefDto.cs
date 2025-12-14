namespace VHub.UserActivities.Application.Contracts.Users;

/// <summary>
/// Пользователь (короткая версия).
/// </summary>
public class UserBriefDto
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Email.
    /// </summary>
    public string Email { get; set; }
}