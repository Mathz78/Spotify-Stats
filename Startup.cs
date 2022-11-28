using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestEase;
using SpotifyStats.Facades;
using SpotifyStats.Interfaces;
using SpotifyStats.Interfaces.RestEase;
using SpotifyStats.Models;
using SpotifyStats.Models.DataBase;
using SpotifyStats.Services;

namespace SpotifyStats
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Spotify Stats", 
                    Version = "v1"
                });
            });
        
            var apiSettings = Configuration.GetSection("ApiSettings").Get<ApiSettings>();

            services.AddSingleton(apiSettings)
                .AddSingleton(RestClient.For<ISpotifyClient>(apiSettings.SpotifyUrls.SpotifyAccounts))
                .AddSingleton(RestClient.For<ISpotiyfUserClient>(apiSettings.SpotifyUrls.SpotifyApi))
                .AddSingleton<IAuthorization, Authorization>()
                .AddSingleton<IUserData, UserData>()
                .AddSingleton<IPlaylist, Playlist>();
            
            services.AddDbContext<SpotifyStatsContext>(opt => opt.UseSqlServer
                (apiSettings.ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify Stats v1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
