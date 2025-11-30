namespace VHub.UserActivities.Application.Contracts.FavoriteOptions;

/// <summary>
/// Ассоциация пользователя с любимой персоной.
/// </summary>
public class UserFavoritePersonAssociationDto
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// ID персоны.
    /// </summary>
    public string PersonId { get; set; }
}