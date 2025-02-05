using System.Net;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

namespace HttpServerLibrary;

internal class Program
{

    static async Task Main(string[] args)
    {

        AppConfigDefault.AppConfig? conf = GetAppConfig();
        
        string prefix = $"http://{conf.Host}:{conf.Port}/";

        HttpServerLibrary server = new HttpServerLibrary(prefix, conf.Static);
        Console.WriteLine($"Server started at {prefix}");
        
        await server.StartAsync();

    }

    private static AppConfigDefault.AppConfig? GetAppConfig()
    {
        if (File.Exists("config.json"))
        {
            var conf = File.ReadAllText("config.json");
            return JsonSerializer.Deserialize<AppConfigDefault.AppConfig>(conf);
        }
        else
        {
            return new AppConfigDefault.AppConfig();
        }
    }

}
