using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyStats.Models.Responses
{
    public class PlaylistsResponse
    {
        // IEnumurable is readonly, and List are not. So if i need to change the values, i should use List.
        
        /// <summary>
        /// List of user's playlists
        /// </summary>
        [JsonProperty("items")]
        public List<PlaylistsItems> Items { get; set; }
        
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
}