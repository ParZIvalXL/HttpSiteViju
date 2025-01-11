namespace Templator.TemplateClasses;

public class FilmCategoryTemplate
{
    public string name { get; set; }
    public string films { get; set; }
    public int order { get; set; }
    
    public FilmCategoryTemplate(string name, string filmIds, int orderId)
    {
        this.name = name;
        films = filmIds;
        order = orderId;
    }

    public FilmCategoryTemplate()
    {
        
    }
}