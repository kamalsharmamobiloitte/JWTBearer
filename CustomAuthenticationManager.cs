using JWTBearerAuthentication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JWTBearerAuthentication
{
    public class CustomAuthenticationManager : IJWTAuthenticationManager
    {
        public readonly TokenStore _tokenStore;
        public CustomAuthenticationManager(TokenStore tokenStore)
        {
            _tokenStore = tokenStore;
        }

        public Dictionary<string, string> userStore = new Dictionary<string, string>()
        { { "user1" , "pass1"} , { "user2" , "pass2"} };

        public string Authenticate(User user)
        {
            if (!userStore.Any(x => x.Key == user.username && x.Value == user.password))
            {
                return null;
            }

            string token = Guid.NewGuid().ToString();

            _tokenStore.tokens.Add(token);

            return token;
           
        }
    }
}