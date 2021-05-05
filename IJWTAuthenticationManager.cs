using JWTBearerAuthentication.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTBearerAuthentication
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(User user);        
    }
}
