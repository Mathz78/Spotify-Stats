using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestEase;
using SpotifyStats.Models;
using SpotifyStats.Models.Responses;

namespace SpotifyStats.Interfaces.RestEase
{
    public interface ISpotifyClient
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }
        
        [Post("api/token")]
        Task<SpotifyTokensResponse> GetUserAsync([Body(BodySerializationMethod.UrlEncoded)] IDictionary<string, string> data);
    }
}