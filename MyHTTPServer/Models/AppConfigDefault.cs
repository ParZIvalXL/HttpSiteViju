namespace HttpServerLibrary;

public class AppConfigDefault
{
    public sealed class AppConfig
    {
        public string Host { get; set; } = "localhost";
        public uint Port { get; set; } = 7574;
        public string Static { get; set; } = "public";
    }
}