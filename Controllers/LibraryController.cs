using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTBearerAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LibraryController : ControllerBase
    {
        public readonly IJWTAuthenticationManager _authenticationManager;
        public LibraryController(IJWTAuthenticationManager manager)
        {
            _authenticationManager = manager;
        }
        // GET: api/Library
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult  Authenticate(User user)
        {
            var token = _authenticationManager.Authenticate(user);

            if(token != null)
            {
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}
