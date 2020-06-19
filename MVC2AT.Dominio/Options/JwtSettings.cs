using System;
using System.Collections.Generic;
using System.Text;

namespace MVC2AT.Dominio.Model.Options
{
  
        public class JwtSettings
        {
            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretKey { get; set; }
            public int ExpiryTimeInSeconds { get; set; }
        }
}
