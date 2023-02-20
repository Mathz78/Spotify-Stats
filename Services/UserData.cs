using System;
using Microsoft.AspNetCore.Http;
using SpotifyStats.Interfaces;
using SpotifyStats.Interfaces.Services;

namespace SpotifyStats.Services
{
    public class UserData : IUserData
    {
        private readonly IJwtService _jwtService;

        public UserData(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        private const string SPOTIFY_ACCESS_TOKEN_COOKIE_NAME = "SpotifyAccessToken";

        public void CreateTokensCookie(HttpContext context, string accessToken, int expirationTime)
        {
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddSeconds(expirationTime)
            };

            var jwtToken = _jwtService.GenerateJwtToken(accessToken, expirationTime);
            
            context.Response.Cookies.Append(SPOTIFY_ACCESS_TOKEN_COOKIE_NAME, accessToken, cookies);
        }

        public string VerifyExistingToken(HttpContext context)
        {
            try
            {
                var token = context.Request.Cookies[SPOTIFY_ACCESS_TOKEN_COOKIE_NAME].ToString();

                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}