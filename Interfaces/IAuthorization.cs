using Microsoft.AspNetCore.Http;

namespace SpotifyStats.Interfaces
{
    public interface IAuthorization
    {
        public string Login();

        public void Callback(string code, string state, HttpContext context);
    }
}