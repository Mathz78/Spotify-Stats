using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using RestEase;
using SpotifyStats.Interfaces;
using SpotifyStats.Interfaces.RestEase;

namespace SpotifyStats.Facades
{
    public class Playlist : IPlaylist
    {
        private readonly IUserData _userData;

        public Playlist(IUserData userData)
        {
            _userData = userData;
        }

        public object GetAllPlaylists(HttpContext context)
        {
            var api = RestClient.For<ISpotiyfUserClient>("https://api.spotify.com");
            api.Authorization = new AuthenticationHeaderValue("Bearer", _userData.VerifyExistingToken(context));

            var userInfo = api.GetUserInfoAsync().Result;
            var userPlaylists = api.GetUserPlaylistsAsync().Result;

            return new object();
        }
    }
}