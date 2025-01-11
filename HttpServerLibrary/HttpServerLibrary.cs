using System.Net;
using System.Net.Mail;
using HttpServerLibrary.Core;
using HttpServerLibrary.Handlers;

namespace HttpServerLibrary;

/// <summary>
/// Основной класс сервера
/// </summary>
public class HttpServerLibrary
{
    
    private readonly string file;
    private readonly string path = Directory.GetCurrentDirectory();
    private readonly string _staticDirectoryPath;
    private readonly string _prefix;
    //Название документа на который идет перенаправление при ошибке get
    private readonly string _notFound = "404.html";
    //
    private readonly Handler _staticFilesHandler = new StaticFilesHandler();
    //Обработчик конечных точек
    private readonly Handler _endPointsHandler = new EndPointsHandler();
    
    //Класс сервера
    private HttpListener _listener = new HttpListener();

    /// <summary>
    /// Конструктор класса сервера
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="File"></param>
    public HttpServerLibrary(string prefix, string File)
    {
        file = File;
        _prefix = prefix;
        _listener.Prefixes.Add(_prefix);
        _staticDirectoryPath = Path.Combine(path, file);
        Console.WriteLine(_staticDirectoryPath);

    }

    /// <summary>
    /// Метод запуск сервера
    /// </summary>
    public async Task StartAsync()
    {
        _listener.Start();
        Console.WriteLine("Server Started");
        Console.WriteLine(_staticDirectoryPath);
        //Цикл обработки запросов
        while (_listener.IsListening)
        {
            //Получение запроса и формирование информации о нем
            var context = await _listener.GetContextAsync();
            var httpContext = new HttpRequestContext(context);
            //Передача запроса в обработку
            await ProcessRequestAsync(httpContext);
        }
    }

    private async Task ProcessRequestAsync(HttpRequestContext context)
    {
        // Передача запроса на обработку конечным точкам
        _staticFilesHandler.Successor = _endPointsHandler;
        _staticFilesHandler.HandleRequest(context);
        
        // Чтение запроса
        /*var request = context.Request;
        string path = request.Url.AbsolutePath;

        string filePath = Path.Combine(_staticDirectoryPath, path);

        //Console.WriteLine($"Запрос к URL: {request.Url}");
        Console.WriteLine($"Запрос {request.HttpMethod}");
        if (request.HttpMethod == "POST")
        {
            //ProcessPostMethod(context);
            return;
        }
        string contentType;
        string fileExtension = Path.GetExtension(filePath).ToLower();
        
        Console.WriteLine(filePath);
        
        if (path.EndsWith("/"))
        {
            contentType = "text/html";
        }else
        {
            switch (fileExtension)
            {
                case (".html"):
                    contentType = "text/html";
                    break;
                case (".css"):
                    contentType = "text/css";
                    break;
                case (".jpg"):
                case (".jpeg"):
                    contentType = "image/jpeg";
                    break;
                case (".png"):
                    contentType = "image/png";
                    break;
                default:
                    contentType = "application/octet-stream";
                    break;
            }
        }

        // Формирование ответа
        try
        {
            bool isRequestEmpty = path == "/";
            path =  isRequestEmpty ? $"/index.html" : path;
            var lastWord = path.Split("/")[^1];
            string returnPath = path;
            //Console.WriteLine(lastWord);
            switch (lastWord)
            {
                case ("login"):
                case ("lol"):
                case ("home-work"):
                case ("requests"):
                    returnPath = path + ".html";
                    break;
                default:
                    break;
            }

            returnPath = returnPath.Replace("/", "\\");
            
            if(File.Exists(_staticDirectoryPath + returnPath) && contentType == "application/octet-stream")
                contentType = "text/html";
            var responseFile = await File.ReadAllBytesAsync(_staticDirectoryPath + returnPath);
            
            //byte[] buffer = Encoding.UTF8.GetBytes(responseFile);
            context.Response.ContentLength64 = responseFile.Length;
            context.Response.ContentType = contentType;
            context.Response.OutputStream.Write(responseFile, 0, responseFile.Length);

            // Закрываем ответ
            context.Response.OutputStream.Close();
        }
        catch(Exception e)
        {
            //Console.WriteLine(e.Message);
            var responseFile = await File.ReadAllBytesAsync(_staticDirectoryPath + "/" + _notFound);

            //byte[] buffer = Encoding.UTF8.GetBytes(responseFile);
            context.Response.ContentLength64 = responseFile.Length;
            context.Response.ContentType = _notFound;
            context.Response.OutputStream.Write(responseFile, 0, responseFile.Length);

            // Закрываем ответ
            context.Response.OutputStream.Close();
        }*/
    }

    
}
