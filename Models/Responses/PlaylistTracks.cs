using System;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json;

namespace SpotifyStats.Models.Responses
{
    public class PlaylistTracks
    {
        // IEnumurable is readonly, and List are not. So if i need to change the values, i should use List.
        
        /// <summary>
        /// List of user's playlists
        /// </summary>
        [JsonProperty("items")]
        public List<PlaylistTracksItems> Items { get; set; }
        
        /// <summary>
        /// Get the next link to request if the response is too large
        /// </summary>
        public string Next { get; set; }
        
        /// <summary>
        /// Get the previous link to request if the response is too large
        /// </summary>
        public string Previous { get; set; }
        
        /// <summary>
        /// Total number of User's playlists 
        /// </summary>
        public int Total { get; set; }
    }

    public class PlaylistTracksItems
    {
        [JsonProperty("track")]
        public TrackName Name { get; set; }
        
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; } 
    }

    public class TrackName
    {
        public string Name { get; set; }   
    }
}