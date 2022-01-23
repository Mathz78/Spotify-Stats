using Newtonsoft.Json;

namespace SpotifyStats.Models.Responses
{
    public class UserProfileResponse
    {
        [JsonProperty("display_name")]
        public string Name { get; set; }
        
        public string Id { get; set; }
    }
}