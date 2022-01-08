namespace SpotifyStats.Models
{
    public class ApiSettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public string BaseSpotifyUrl { get; set; }

        public string SpotifyScope { get; set; }
    }
}