using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace JWTBearerAuthentication
{
    public class BasicAuthenticaitonOptions : AuthenticationSchemeOptions
    {

    }

    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticaitonOptions>
    {

        public readonly TokenStore _tokenStore;
        public CustomAuthenticationHandler(IOptionsMonitor<BasicAuthenticaitonOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, TokenStore tokenStore) : base(options, logger, encoder, clock)
        {
            _tokenStore = tokenStore;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.Run(()=>AuthenticateResult.Fail("Authentication fail"));

            var header =  Request.Headers["Authorization"].ToString();

            if(string.IsNullOrEmpty(header))
                return Task.Run(() => AuthenticateResult.Fail("Authentication fail"));

            var validatetoken = header.Substring("bearer".Length).Trim();

            if (_tokenStore.tokens.Contains(validatetoken))
            {
                var claimsIdentity = new ClaimsIdentity(
                    new List<Claim>() { 
                        new Claim(ClaimTypes.Name, validatetoken) 
                    },Scheme.Name);
                var principal = new GenericPrincipal(claimsIdentity,null);
                var ticket = new AuthenticationTicket(principal,Scheme.Name);
                return Task.Run(() => AuthenticateResult.Success(ticket));                
            }
            else
            {
                return Task.Run(() => AuthenticateResult.Fail("Authentication fail"));
            }
        }
    }
}
