using Core.Entity.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyription;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper //burada yapacağımız ilk iş configtem token'ı okumak olacak
    {
        private IConfiguration Configuration { get; }//microsoft extensiondan çağırmamız lazım NUGET
        private TokenOptions _tokenOptions; //config dosyasında tokenoptions alanı olacak , o alanı bu nesneye aktarcam
        private DateTime _accessTokenExpiration;


        //config dosyası vasıtasıyla, yapıyı okuyacağız
        public JwtHelper(IConfiguration configuration)//appsettings teki ayarları configurasyon ayarları ile okuyacağız
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();//tokenoptions kısmını oku, tokenoptions nesnesiini al _tokenOptionsa set et
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);// algoriymayı kullanarak şifreli token oluşturcaz, o tokenı oluştururken 1 tane anahtara ihtiyacımız var(özel bir anahtar),encyription klasör içinde,symmetricSecurtykey kullanarak static olarak üretildi

            //signingCredentials: securtityKey ve algoritmamızı belirlediğimiz bir nesnedir
            var signingCredentials = SingingCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: tokenOptions.Issuer,
                    audience:tokenOptions.Audience,                   
                    expires:_accessTokenExpiration,//tokenOptions.AccessTokenExpiration =dakika olarak ayarla..
                    notBefore: System.DateTime.Now,//eğer token expressin bilgisi şuandan önce ise geçerli değil, şart bu
                    claims:SetClaims(user,operationClaims),
                    signingCredentials:signingCredentials
                );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)//claim,security claims den geliyor
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }

        //claim koleksiyonu varsa ben buna extension yazarak daha kullanışlı hale getirebiliriz
    }
}
