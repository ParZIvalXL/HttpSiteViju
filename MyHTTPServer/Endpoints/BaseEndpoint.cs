using System.Data.SqlClient;
using HttpServerLibrary;
using HttpServerLibrary.Attributes;
using HttpServerLibrary.HttpResponce;
using HttpServerLibrary.HttpResponse;
using MyHTTPServer.Models;
using MyHTTPServer.Sessions;
using MyORMLibrary;
using Templator;
using Templator.TemplateClasses;

namespace MyHTTPServer.Endpoints;

public class BaseEndpoint : BaseEndPoint
{
    [Get("")]
    public IHttpResponceResult Index()
    {
        var templator = new CustomTemplator();
        var file = File.ReadAllText(
            @"public/index.html");
        if (SessionStorage.IsAuthorized(Context))
        {
            file = templator.RenderLogged(file);
        }
        return Html(file);
    }

    [Get("film")]
    public IHttpResponceResult Film(string filmId)
    {
        int id = int.Parse(filmId);
        
        var orm = new ORMContext<FilmInfo>(new SqlConnection(@"Data Source=localhost; Initial Catalog=Film; User ID=sa;Password=P@ssw0rd; TrustServerCertificate=true;"));
        var filmCardTemplate = orm.Where(f => f.id == id).ToList().Find(f=> f.id == id);
        Console.WriteLine("Film is null?" + filmCardTemplate == null);
        if (filmCardTemplate == null || filmCardTemplate.id == 0)
        {
            return Redirect("/");
        }
        var templator = new CustomTemplator();
        return Html(templator.RenderFilmSite(
            new FilmInfoTemplate(
                filmCardTemplate.FilmName,
                filmCardTemplate.FullFilmName,
                filmCardTemplate.FilmPoster,
                filmCardTemplate.FilmNamePoster,
                filmCardTemplate.FilmYear,
                filmCardTemplate.FilmCountries,
                filmCardTemplate.FilmDuration,
                filmCardTemplate.FilmAgeLimit,
                filmCardTemplate.FilmGenres,
                filmCardTemplate.FilmTagline,
                filmCardTemplate.FilmStarring,
                filmCardTemplate.FilmDescription,
                filmCardTemplate.FilmDirector,
                filmCardTemplate.FilmActors,
                filmCardTemplate.FilmWritters,
                filmCardTemplate.FilmProducers,
                filmCardTemplate.FilmAudioChannels,
                filmCardTemplate.FilmQuality,
                filmCardTemplate.FilmPlotSummary,
                filmCardTemplate.VKLink,
                filmCardTemplate.id)));
    }
}