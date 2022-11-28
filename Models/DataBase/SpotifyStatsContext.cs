using Microsoft.EntityFrameworkCore;

namespace SpotifyStats.Models.DataBase 
{
    public class SpotifyStatsContext : DbContext 
    {
        public DbSet<User> Users { get; set; }

        public SpotifyStatsContext(DbContextOptions<SpotifyStatsContext> options) : base(options) 
        {
        }
     }
}