using System;
using System.Threading.Tasks;

namespace SpotifyStats.Interfaces
{
    public interface IAuthorization
    {
        public string Login();

        public string Callback(string code, string state);
    }
}