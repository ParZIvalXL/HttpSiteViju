namespace HttpServerLibrary.Core.Attributes;

/// <summary>
/// Аттрибут
/// </summary>
public class PostAttribute : Attribute
{
    public string Route { get; } // Ссылка запроса по которому должен выполняться запрос

    /// <summary>
    /// Конструктор атрибута
    /// </summary>
    /// <param name="route">Ссылка запроса по которому должен выполняться запрос</param>
    public PostAttribute(string route)
    {
        Route = route;
    }
}