using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var result = _authorization.Callback(code, state);
        }
    }
}
