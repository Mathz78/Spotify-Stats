using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.Http;
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
        private readonly ISpotiyfUserClient _spotiyfUserClient;

        public Playlist(IUserData userData, ISpotiyfUserClient spotiyfUserClient)
        {
            _userData = userData;
            _spotiyfUserClient = spotiyfUserClient;
        }

        public PlaylistsResponse GetAllPlaylists(HttpContext context)
        {
            _spotiyfUserClient.Authorization = new AuthenticationHeaderValue("Bearer", _userData.VerifyExistingToken(context));

            var userInfo = _spotiyfUserClient.GetUserInfoAsync().Result;
            var userPlaylists = _spotiyfUserClient.GetUserPlaylistsAsync().Result;

            return userPlaylists;
        }

        public List<PlaylistTracksItems> GetPlaylistTracks(HttpContext context, string playlistId)
        {
            var playlistTracks = new List<PlaylistTracksItems>();

            _spotiyfUserClient.Authorization = new AuthenticationHeaderValue("Bearer", _userData.VerifyExistingToken(context));

            var spotifyPlaylistTracksResponse = _spotiyfUserClient.GetPlaylistTracksAsync(playlistId, FIRST_OFFSET_INDEX).Result;
            playlistTracks = AddSongsIntoList(playlistTracks, spotifyPlaylistTracksResponse.Items);
            
            while (spotifyPlaylistTracksResponse.Next != null)
            {
                Uri url = new Uri(spotifyPlaylistTracksResponse.Next);
                spotifyPlaylistTracksResponse = _spotiyfUserClient.GetPlaylistTracksAsync(playlistId, HttpUtility.ParseQueryString(url.Query).Get(OFFSET)).Result;

                playlistTracks = AddSongsIntoList(playlistTracks, spotifyPlaylistTracksResponse.Items);
            }

            return playlistTracks;
        }

        public object GetPlaylistStats(HttpContext context, string playlistId)
        {
            var tracks = GetPlaylistTracks(context, playlistId);
            var years = new Dictionary<string, int>();

            foreach (var track in tracks)
            {
                var songAddedYear = track.AddedAt.Year.ToString();

                if (!years.ContainsKey(songAddedYear)) 
                {
                    years.Add(songAddedYear, default(int));
                }
                
                years[track.AddedAt.Year.ToString()] += 1;
            }
            
            return years;
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