using System.ComponentModel.DataAnnotations;

namespace SpotifyStats.Models 
{
    public class User 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Identifier { get; set; }

        public string AccessToken { get; set; }
    }
}