using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyStats.Interfaces;

namespace SpotifyStats.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorization _authorization;
        private readonly IUserData _userData;

        public AuthorizationController(IAuthorization authorization, IUserData userData)
        {
            _authorization = authorization;
            _userData = userData;
        }

        [HttpGet]
        public RedirectResult Login()
        {
            var url = _authorization.Login();
            
            return Redirect(url);
        }

        [HttpGet]
        [Route("/callback")]
        public RedirectResult Callback([FromQuery(Name = "code")] string code, [FromQuery(Name = "state")] string state)
        {
            _authorization.Callback(code, state, HttpContext);
            return Redirect("stats");
        }
    }
}
