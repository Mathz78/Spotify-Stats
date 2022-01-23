using Microsoft.AspNetCore.Mvc;
using SpotifyStats.Interfaces;

namespace SpotifyStats.Controllers
{
    [ApiController]
    [Route("stats")]
    public class StatsController : ControllerBase
    {
        private IPlaylist _playlist;

        public StatsController(IPlaylist playlist)
        {
            _playlist = playlist;
        }

        [HttpGet]
        public void GetAllPlaylists()
        {
            _playlist.GetAllPlaylists(HttpContext);
        }
    }
}