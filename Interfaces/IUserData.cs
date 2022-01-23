using Microsoft.AspNetCore.Http;

namespace SpotifyStats.Interfaces
{
    public interface IUserData
    {
        /// <summary>
        /// Creates a new cookie to store spotify's token to access user data
        /// </summary>
        /// <param name="context"></param>
        /// <param name="accessToken"></param>
        /// <param name="expirationTime"></param>
        public void CreateTokensCookie(HttpContext context, string accessToken, int expirationTime);

        /// <summary>
        /// Verify and returns sptofiy's token to access user data
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string VerifyExistingToken(HttpContext context);
    }
}