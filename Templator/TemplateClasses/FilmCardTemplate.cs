namespace Templator.TemplateClasses;

public class FilmCardTemplate
{
    public string film_name { get; set; }
    public string film_description { get; set; }
    public string film_image { get; set; }
    public int film_id { get; set; }

    public FilmCardTemplate(string filmName, string filmDescription, string filmImage, int id)
    {
        film_name = filmName;
        film_description = filmDescription;
        film_image = filmImage;
        film_id = id;
    }
}