namespace SpotifyStats.Interfaces.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(string spotifyAccessToken, int tokenExpirationTimeInSeconds);
    }
}