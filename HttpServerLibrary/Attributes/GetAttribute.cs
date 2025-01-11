namespace HttpServerLibrary.Attributes;

/// <summary>
/// Аттрибут запроса GET 
/// </summary>
public class GetAttribute : Attribute
{
    public string Route { get; } // Ссылка запроса по которому должен выполняться запрос

    /// <summary>
    /// Конструктор атрибута
    /// </summary>
    /// <param name="route">Ссылка запроса по которому должен выполняться запрос</param>
    public GetAttribute(string route)
    {
        Route = route;
    }
}