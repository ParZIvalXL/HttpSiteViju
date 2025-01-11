using System.Data.SqlClient;
using HttpServerLibrary.Attributes;
using HttpServerLibrary.HttpResponce;
using MyHTTPServer.Models;
using MyORMLibrary;
using Templator;
using Templator.TemplateClasses;
using FilmCategory = MyHTTPServer.Models.FilmCategory;
using SqlConnection = System.Data.SqlClient.SqlConnection;

namespace MyHTTPServer.Endpoints;

public class DbReaderEndpoint : BaseEndpoint
{
    [Get("users")]
    public void GetUsers()
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=PersonsDB;Users ID=sa;Password=P@ssw0rd;";
 
        string sqlExpression = "SELECT * FROM Users";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();
 
            if(reader.HasRows) // если есть данные
            {
                // выводим названия столбцов
                Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
 
                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
                    object name = reader.GetValue(1);
                    object age = reader.GetValue(2);
 
                    Console.WriteLine("{0} \t{1} \t{2}", id, name, age);
                }
            }
         
            reader.Close();
        }
             
        Console.Read();
    }

    [Get("filmCategory")]
    public IHttpResponceResult GetFilmCategory(string order)
    {
        Console.WriteLine(order);
        string connectionString = @"Data Source=localhost; Initial Catalog=Film; User ID=sa;Password=P@ssw0rd; TrustServerCertificate=true;";
        var customTemplator = new CustomTemplator();
        var ordering = int.Parse(order);
        
        var orm = new ORMContext<FilmCategory>( new SqlConnection(connectionString));
        
        FilmCategory? filmCategories;
        try
        {
             filmCategories = orm.Where(f => f.Ordering == ordering).ToList().Find(f => f.Ordering == ordering);
             Console.WriteLine("Ajax trying to get: " + ordering + " Found " + filmCategories.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        FilmCategoryTemplate filmCategory = new FilmCategoryTemplate(filmCategories.Name, 
            filmCategories.Ids, 
            filmCategories.Ordering);
        
        ORMContext<FilmCardInfo> ormContext = new ORMContext<FilmCardInfo>(new SqlConnection(connectionString));
        var filmsIds = filmCategories.Ids.Split(',').Select(int.Parse);
        var filmCards = ormContext.GetByAll().Where(w => filmsIds.Contains(w.Id))
            .Select(s => new FilmCardTemplate(s.Name, s.Description, s.Img, s.FilmId)).ToList();
        
        var res = customTemplator.CreateCategoryList(order, filmCards, filmCategory);
        return Html(res);
    }
}