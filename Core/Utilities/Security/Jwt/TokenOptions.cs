using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenOptions//jwt için gerekli özellikler, api de setting içinde tutulacak - nesne olarak map edip o nesne üzerinde map edeceğiz. sonraki adım JWTHelper class oluştur
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
