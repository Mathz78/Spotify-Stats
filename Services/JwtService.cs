using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using SpotifyStats.Interfaces.Services;
using SpotifyStats.Models;

namespace SpotifyStats.Services
{
    public class JwtService : IJwtService
    {
        private readonly ApiSettings _apiSettings;

        public JwtService(ApiSettings apiSettings)
        {
            _apiSettings = apiSettings;
        }

        public  string GenerateJwtToken(string spotifyAccessToken, int tokenExpirationTimeInSeconds)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_apiSettings.JwtSecretToken);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, spotifyAccessToken)
                }),
                Expires = DateTime.UtcNow.AddSeconds(tokenExpirationTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}