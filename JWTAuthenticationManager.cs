using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWTBearerAuthentication.Controllers;
using Microsoft.IdentityModel.Tokens;

namespace JWTBearerAuthentication
{
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string keyToEncrypt = string.Empty;

        public JWTAuthenticationManager(string key)
        {
            keyToEncrypt = key;
        }

        public Dictionary<string, string> userStore = new Dictionary<string, string>() 
        { { "user1" , "pass1"} , { "user2" , "pass2"} };


        public string Authenticate(User user)
        {
            if(!userStore.Any(x=> x.Key == user.username && x.Value == user.password))
            {
                return null;
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.username)
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(keyToEncrypt)), SecurityAlgorithms.HmacSha256)
                
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
