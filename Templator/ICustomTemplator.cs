namespace Templator;

public interface ICustomTemplator
{
    public string GetHtmlByTemplate(string template, string name);
    public string GetHtmlByTemplate<T>(string template, T toReplace);
}