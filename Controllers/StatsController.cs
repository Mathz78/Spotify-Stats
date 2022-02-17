using Microsoft.AspNetCore.Components.Routing;
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
        public IActionResult GetAllPlaylists()
        {
            return Ok(_playlist.GetAllPlaylists(HttpContext));
        }

        [HttpGet]
        [Route("playlist/tracks")]
        public IActionResult GetPlaylistTracks([FromQuery(Name = "id")] string playlistId)
        {
            return Ok(_playlist.GetPlaylistTracks(HttpContext, playlistId));
        }
    }
}