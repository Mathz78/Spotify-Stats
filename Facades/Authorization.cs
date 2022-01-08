using System;
using System.Collections.Specialized;
using Microsoft.Extensions.Options;
using SpotifyStats.Interfaces;
using SpotifyStats.Models;

namespace SpotifyStats.Facades
{
    public class Authorization : IAuthorization
    {
        private ApiSettings _apiSettings;

        private const string RESPONSE_TYPE = "response_type";
        private const string CLIENT_ID = "client_id";
        private const string SCOPE = "scope";
        private const string REDIRECT_URI = "redirect_uri";
        private const string STATE = "state";
        private const string RESPONSE_TYPE_VALUE = "code";

        private string _clientIdValue;
        private string _redirectUriValue;
        private string _baseUrl;
        private string _scope;

        public Authorization(IOptions<ApiSettings> apiSettings)
        {
            _apiSettings = apiSettings.Value;

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