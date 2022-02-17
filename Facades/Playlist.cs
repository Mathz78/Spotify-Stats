using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.Http;
using RestEase;
using SpotifyStats.Interfaces;
using SpotifyStats.Interfaces.RestEase;
using SpotifyStats.Models.Responses;

namespace SpotifyStats.Facades
{
    public class Playlist : IPlaylist
    {
        private const string FIRST_OFFSET_INDEX = "0";
        private const string OFFSET = "offset";

        private readonly IUserData _userData;

        public Playlist(IUserData userData)
        {
            _userData = userData;
        }

        public PlaylistsResponse GetAllPlaylists(HttpContext context)
        {
            var api = RestClient.For<ISpotiyfUserClient>("https://api.spotify.com");
            api.Authorization = new AuthenticationHeaderValue("Bearer", _userData.VerifyExistingToken(context));

            var userInfo = api.GetUserInfoAsync().Result;
            var userPlaylists = api.GetUserPlaylistsAsync().Result;

            return userPlaylists;
        }

        public List<PlaylistTracksItems> GetPlaylistTracks(HttpContext context, string playlistId)
        {
            var playlistTracks = new List<PlaylistTracksItems>();

            var api = RestClient.For<ISpotiyfUserClient>("https://api.spotify.com");
            api.Authorization = new AuthenticationHeaderValue("Bearer", _userData.VerifyExistingToken(context));

            var spotifyPlaylistTracksResponse = api.GetPlaylistTracksAsync(playlistId, FIRST_OFFSET_INDEX).Result;
            playlistTracks = AddSongsIntoList(playlistTracks, spotifyPlaylistTracksResponse.Items);
            
            while (spotifyPlaylistTracksResponse.Next != null)
            {
                Uri url = new Uri(spotifyPlaylistTracksResponse.Next);
                spotifyPlaylistTracksResponse = api.GetPlaylistTracksAsync(playlistId, HttpUtility.ParseQueryString(url.Query).Get(OFFSET)).Result;

                playlistTracks = AddSongsIntoList(playlistTracks, spotifyPlaylistTracksResponse.Items);
            }

            return playlistTracks;
        }

        private List<PlaylistTracksItems> AddSongsIntoList(List<PlaylistTracksItems> tracks, List<PlaylistTracksItems> tracksToAdd)
        {
            foreach (var track in tracksToAdd)
            {
                tracks.Add(track);
            }

            return tracks;
        }
    }
}