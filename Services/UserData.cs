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
            CookieOptions cookies = new CookieOptions();

            cookies.Expires = DateTimeOffset.Now.AddSeconds(expirationTime);
            context.Response.Cookies.Append(SPOTIFY_ACCESS_TOKEN_COOKIE_NAME, accessToken, cookies);
        }

        public string VerifyExistingToken(HttpContext context)
        {
            return context.Request.Cookies[SPOTIFY_ACCESS_TOKEN_COOKIE_NAME].ToString();
        }
    }
}