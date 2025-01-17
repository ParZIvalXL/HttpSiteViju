using System.Data.SqlClient;
using System.Net;
using System.Web;
using HttpServerLibrary;
using HttpServerLibrary.Attributes;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.HttpResponce;
using HttpServerLibrary.HttpResponse;
using MyHTTPServer.Endpoints;
using MyHTTPServer.Models;
using MyHTTPServer.Sessions;
using MyORMLibrary;
using Templator;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;

namespace MyHTTPServer.EndPoints;

public class AuthEndPoint : BaseEndPoint
{
    [Get("login")]
    public IHttpResponceResult Login(string error)
    {
        var customTemplator = new CustomTemplator();
        if (SessionStorage.IsAuthorized(Context))
        {
            Console.WriteLine("redirecting...");
        }

        var file = File.ReadAllText(
            @"public/login.html");
        switch (error)
        {
            case("incorrectLoginInput"):
                file = file.Replace("<!--error-->","<p class=\"_passwordError_8gnij_9\">Вы должны ввести логин</p>");
                break;
            case("incorrectPasswordInput"):
                file = file.Replace("<!--error-->","<p class=\"_passwordError_8gnij_9\">Вы должны ввести пароль</p>");
                break;
            case("notFound"):
                file = file.Replace("<!--error-->","<p class=\"_passwordError_8gnij_9\">Такого пользователя не существует, зарегистрируйтесь</p>");
                break;
            case("wrongPassword"):
                file = file.Replace("<!--error-->","<p class=\"_passwordError_8gnij_9\">Неверный пароль </p>");
                break;
        }
        
        
        return Html(file);
    }
    
    [Get("register")]
    public IHttpResponceResult Register()
    {
        return Html(File.ReadAllText(
            @"public/register.html"));
    }

    [Post("login")]
    public IHttpResponceResult Login(string login, string password)
    {
        Console.WriteLine($"Login: {login}, password: {password}");
        if (login.Length == 0)
        {
            return Redirect("/login?error=incorrectLoginInput");
        }
        
        if (password.Length == 0)
        {
            return Redirect("/login?error=incorrectPasswordInput");
        }
        string connectionString = @"Data Source=localhost; Initial Catalog=Film; User ID=sa;Password=P@ssw0rd; TrustServerCertificate=true;";
        var dBcontext = new ORMContext<Users>(new System.Data.SqlClient.SqlConnection(connectionString));
        var user = dBcontext.CheckUserByData(login);

        if (user == null)
        {
            var phone = login;
            if (user == null)
            {
                Console.WriteLine("Users not found");
                return Redirect("/login?error=notFound");
            }
        }
        
        if (user.Password != password)
        {
            Console.WriteLine("Wrong password");
            return Redirect("/login?error=wrongPassword");
        }
        
        Console.WriteLine("Users found");
        string token = Guid.NewGuid().ToString();
        Cookie cookie = new Cookie("session-token", token);
        Context.Response.SetCookie(cookie);
        SessionStorage.SaveSession(token, user.Id.ToString());
        return Redirect("/");
    }
    
    
}