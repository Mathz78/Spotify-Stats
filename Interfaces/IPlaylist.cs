using Microsoft.AspNetCore.Http;

namespace SpotifyStats.Interfaces
{
    public interface IPlaylist
    {
        public object GetAllPlaylists(HttpContext context);
    }
}