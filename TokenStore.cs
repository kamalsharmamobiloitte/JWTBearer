using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTBearerAuthentication
{
    public class TokenStore
    {
        public List<string> tokens { get; set; } = new List<string>();
    }
}
