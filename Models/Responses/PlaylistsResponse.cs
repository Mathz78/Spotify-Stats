using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyStats.Models.Responses
{
    public class PlaylistsResponse
    {
        [JsonProperty("items")]
        public List<object> Items { get; set; }
    }
}