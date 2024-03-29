namespace SpotifyStats.Models
{
    public class SpotifySettings
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public string BaseSpotifyUrl { get; set; }

        public string SpotifyScope { get; set; }

        public SpotifyUrls SpotifyUrls { get; set; }
    }
}