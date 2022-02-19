using System;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using SpotifyStats.Interfaces;
using SpotifyStats.Interfaces.RestEase;
using SpotifyStats.Models;


namespace SpotifyStats.Facades
{
    public class Authorization : IAuthorization
    {
        private readonly ApiSettings _apiSettings;
        private readonly ISpotifyClient _spotifyClient;
        private readonly IUserData _userData;

        private const string RESPONSE_TYPE = "response_type";
        private const string CLIENT_ID = "client_id";
        private const string SCOPE = "scope";
        private const string REDIRECT_URI = "redirect_uri";
        private const string STATE = "state";
        private const string RESPONSE_TYPE_VALUE = "code";

        private readonly string _clientSecretValue;
        private readonly string _clientIdValue;
        private readonly string _redirectUriValue;
        private readonly string _baseUrl;
        private readonly string _scope;

        public Authorization(ApiSettings apiSettings, IUserData userData, ISpotifyClient spotifyClient)
        {
            _apiSettings = apiSettings;
            _spotifyClient = spotifyClient;
            
            _clientSecretValue = _apiSettings.ClientSecret;
            _clientIdValue = _apiSettings.ClientId;
            _redirectUriValue = _apiSettings.RedirectUri;
            _baseUrl = _apiSettings.BaseSpotifyUrl;
            _scope = _apiSettings.SpotifyScope;

            _userData = userData;
        }

        public string Login()
        {
            var state = generateRandomString();
            var url = buildUrl(_baseUrl, _clientIdValue, _scope, _redirectUriValue, state);
            
            return url;
        }
        
        public void Callback(string code, string state, HttpContext context)
        {
            var authOptions = new Dictionary<string, string> {
                {"code", code},
                {"redirect_uri", _redirectUriValue},
                {"grant_type", "authorization_code"}
            };

            var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientIdValue}:{_clientSecretValue}"));
            _spotifyClient.Authorization = new AuthenticationHeaderValue("Basic", authorization);
            
            var clientTokens = _spotifyClient.GetUserAsync(authOptions).Result;

            _userData.CreateTokensCookie(context, clientTokens.AccessToken, clientTokens.Expiration);
        }

        private string buildUrl(string baseUrl, string clientIdValue, string scopeValue, string redirectUriValue, string stateValue)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add(RESPONSE_TYPE, RESPONSE_TYPE_VALUE);
            queryString.Add(CLIENT_ID, clientIdValue);
            queryString.Add(SCOPE, scopeValue);
            queryString.Add(REDIRECT_URI, redirectUriValue);
            queryString.Add(STATE, stateValue);

            var url = baseUrl + queryString;
            
            return url;
        }

        private string generateRandomString()
        {
            var random = new Random();
            var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
            string randomString = "";

            for (var i = 0; i < 16; i++)
            {
                randomString += alphabet[random.Next(0, alphabet.Length)];
            }
            
            return randomString;
        }
    }   
}