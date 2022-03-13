using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SpotifyStats.Models.Responses;

namespace SpotifyStats.Interfaces
{
    public interface IPlaylist
    {
        public PlaylistsResponse GetAllPlaylists(HttpContext context);

        public List<PlaylistTracksItems> GetPlaylistTracks(HttpContext context, string id);

        public object GetPlaylistStats(HttpContext context, string id);
    }
}