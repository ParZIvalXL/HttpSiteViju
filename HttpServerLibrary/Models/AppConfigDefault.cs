namespace HttpServerLibrary.Models;

public class AppConfigDefault
{
    public sealed class AppConfig
    {
        public string Host { get; set; } = "localhost";
        public uint Port { get; set; } = 8785;
        public string Static { get; set; } = "public";
    }
}