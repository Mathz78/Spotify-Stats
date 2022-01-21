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
        private IAuthorization _authorization;

        public AuthorizationController(IAuthorization authorization)
        {
            _authorization = authorization;
        }

        [HttpGet]
        public RedirectResult Login()
        {
            var url = _authorization.Login();
            
            return Redirect(url);
        }

        [HttpGet]
        [Route("/callback")]
        public void Callback([FromQuery(Name = "code")] string code, [FromQuery(Name = "state")] string state)
        {
            _authorization.Callback(code, state, HttpContext);
        }
    }
}
