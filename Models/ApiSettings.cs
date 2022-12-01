namespace SpotifyStats.Models
{
    public class ApiSettings
    {
        public SpotifySettings SpotifySettings { get; set; }

        public string JwtSecretToken { get; set; }
    }
}