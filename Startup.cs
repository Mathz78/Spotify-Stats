using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestEase;
using SpotifyStats.Facades;
using SpotifyStats.Interfaces;
using SpotifyStats.Interfaces.RestEase;
using SpotifyStats.Models;
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

            var apiSettings = Configuration.GetSection("ApiSettings").Get<ApiSettings>();

            services.AddSingleton(apiSettings)
                .AddSingleton(RestClient.For<ISpotifyClient>(apiSettings.SpotifyUrls.SpotifyAccounts))
                .AddSingleton(RestClient.For<ISpotiyfUserClient>(apiSettings.SpotifyUrls.SpotifyApi))
                .AddSingleton<IAuthorization, Authorization>()
                .AddSingleton<IUserData, UserData>()
                .AddSingleton<IPlaylist, Playlist>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
