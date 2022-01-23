using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;
using SpotifyStats.Models;
using SpotifyStats.Models.Responses;

namespace SpotifyStats.Interfaces.RestEase
{
    public interface ISpotiyfUserClient
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }
        
        [Get("v1/me")]
        Task<UserProfileResponse> GetUserInfoAsync();
        
        [Get("v1/me/playlists?limit=50")]
        Task<PlaylistsResponse> GetUserPlaylistsAsync();

        [Get("v1/playlists/{playlistId}/tracks?{offset}")]
        Task<object> GetPlaylistTracksAsync([Path] string playlistId, [Path] string offset);
    }
}