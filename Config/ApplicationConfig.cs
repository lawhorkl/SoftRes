namespace SoftRes.Config
{
    public class ApplicationConfig
    {
        public AuthConfig Auth { get; set; }
        public GameDateAPIConfig GameDataAPI { get; set; }
    }

    public class AuthConfig
    {
        public string Uri { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Key { get; set; }
    }

    public class GameDateAPIConfig
    {
        public string Uri { get; set; }
    }
}