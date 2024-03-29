using Microsoft.AspNetCore.Mvc;
using SpotifyStats.Interfaces;

namespace SpotifyStats.Controllers
{
    [ApiController]
    [Route("stats")]
    public class StatsController : ControllerBase
    {
        private readonly IPlaylist _playlist;
        private readonly IUserData _userData;

        public StatsController(IPlaylist playlist, IUserData userData)
        {
            _playlist = playlist;
            _userData = userData;
        }

        [HttpGet]
        public IActionResult GetAllPlaylists()
        {
            if (_userData.VerifyExistingToken(HttpContext) != null)
            {
                return Ok(_playlist.GetAllPlaylists(HttpContext));
            }

            return Redirect("/");
        }

        [HttpGet]
        [Route("playlist/tracks")]
        public IActionResult GetPlaylistTracks([FromQuery(Name = "id")] string playlistId)
        {
            if (_userData.VerifyExistingToken(HttpContext) != null)
            {
                return Ok(_playlist.GetPlaylistTracks(HttpContext, playlistId));
            }

            return Redirect("/");
        }

        [HttpGet]
        [Route("playlist/stats")]
        public IActionResult GetPlaylistStats([FromQuery(Name = "id")] string playlistId)
        {
            if (_userData.VerifyExistingToken(HttpContext) != null)
            {
                return Ok(_playlist.GetPlaylistStats(HttpContext, playlistId));
            }

            return Redirect("/");
        }
    }
}