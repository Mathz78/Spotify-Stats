using Newtonsoft.Json;

namespace SpotifyStats.Models.Responses
{
    public class PlaylistsItems
    {
        public bool Collaborative { get; set; }
        
        public string Description { get; set; }

        public string Id { get; set; }
        
        public string Name { get; set; }
    }
}