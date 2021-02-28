using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT // JASONWEBTOKEN  
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
