using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using RestEase;
using SpotifyStats.Interfaces;
using SpotifyStats.Models;

namespace SpotifyStats.Facades
{
    public class Authorization : IAuthorization
    {
        private ApiSettings _apiSettings;
        private ISpotifyClient _spotifyClient;

        private const string RESPONSE_TYPE = "response_type";
        private const string CLIENT_ID = "client_id";
        private const string SCOPE = "scope";
        private const string REDIRECT_URI = "redirect_uri";
        private const string STATE = "state";
        private const string RESPONSE_TYPE_VALUE = "code";

        private string _clientSecretValue;
        private string _clientIdValue;
        private string _redirectUriValue;
        private string _baseUrl;
        private string _scope;

        public Authorization(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;

            _clientSecretValue = _apiSettings.ClientSecret;
            _clientIdValue = _apiSettings.ClientId;
            _redirectUriValue = _apiSettings.RedirectUri;
            _baseUrl = _apiSettings.BaseSpotifyUrl;
            _scope = _apiSettings.SpotifyScope;
        }

        public string Login()
        {
            var state = generateRandomString();
            var url = buildUrl(_baseUrl, _clientIdValue, _scope, _redirectUriValue, state);

            return url;
        }
        
        public string Callback(string code,  string state)
        {
            var api = RestClient.For<ISpotifyClient>("https://accounts.spotify.com");

            var authOptions = new Dictionary<string, string> {
                {"code", code},
                {"redirect_uri", _redirectUriValue},
                {"grant_type", "authorization_code"}
            };

            var authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_clientIdValue}:{_clientSecretValue}"));
            api.Authorization = new AuthenticationHeaderValue("Basic", authorization);
            
            var user = api.GetUserAsync(authOptions).Result;

            return user.ToString();
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