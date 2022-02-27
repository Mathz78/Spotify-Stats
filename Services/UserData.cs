using System;
using Microsoft.AspNetCore.Http;
using SpotifyStats.Interfaces;

namespace SpotifyStats.Services
{
    public class UserData : IUserData
    {
        private const string SPOTIFY_ACCESS_TOKEN_COOKIE_NAME = "SpotifyAccessToken";

        public void CreateTokensCookie(HttpContext context, string accessToken, int expirationTime)
        {
            CookieOptions cookies = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddSeconds(expirationTime)
            };

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