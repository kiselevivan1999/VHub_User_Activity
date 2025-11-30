using System.ComponentModel;

namespace VHub.UserActivities.Common.Enums;

/// <summary>
/// Жанр фильма.
/// </summary>
public enum GenreType : short
{
    /// <summary>
    /// Неизвестно.
    /// </summary>
    [Description("Неизвестно")]
    Unknown = 0,

    /// <summary>
    /// Боевик.
    /// </summary>
    [Description("Боевик")]
    ActionMovie = 1,

    /// <summary>
    /// Военный.
    /// </summary>
    [Description("Военный")]
    Military = 2,

    /// <summary>
    /// Детектив.
    /// </summary>
    [Description("Детектив")]
    Detective = 3,

    /// <summary>
    /// Детский.
    /// </summary>
    [Description("Детский")]
    Children = 4,

    /// <summary>
    /// Документальный.
    /// </summary>
    [Description("Документальный")]
    Documentary = 5,

    /// <summary>
    /// Драма.
    /// </summary>
    [Description("Драма")]
    Drama = 6,

    /// <summary>
    /// Исторический.
    /// </summary>
    [Description("Исторический")]
    Historical = 7,

    /// <summary>
    /// Комедия.
    /// </summary>
    [Description("Комедия")]
    Comedy = 8,

    /// <summary>
    /// Криминал.
    /// </summary>
    [Description("Криминал")]
    Crime = 9,

    /// <summary>
    /// Мелодрама.
    /// </summary>
    [Description("Мелодрама")]
    Melodrama = 10,

    /// <summary>
    /// Мультфильм.
    /// </summary>
    [Description("Мультфильм")]
    Cartoon = 11,

    /// <summary>
    /// Мюзикл.
    /// </summary>
    [Description("Мюзикл")]
    Musical = 12,

    /// <summary>
    /// Приключения.
    /// </summary>
    [Description("Приключения")]
    Adventure = 13,

    /// <summary>
    /// Семейный.
    /// </summary>
    [Description("Семейный")]
    Family = 14,

    /// <summary>
    /// Спорт.
    /// </summary>
    [Description("Спорт")]
    Sports = 15,

    /// <summary>
    /// Триллер.
    /// </summary>
    [Description("Триллер")]
    Thriller = 16,

    /// <summary>
    /// Ужасы.
    /// </summary>
    [Description("Ужасы")]
    Horror = 17,

    /// <summary>
    /// Фантастика.
    /// </summary>
    [Description("Фантастика")]
    Fantastic = 18,

    /// <summary>
    /// Фэнтези.
    /// </summary>
    [Description("Фэнтези")]
    Fantasy = 19,
}