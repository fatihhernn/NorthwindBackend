using Core.Entity.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        //register olmak demek-kullanıcının sisteme kayıt olması demektir-kayıt olması sonucunda kullanıcya result veriliyor
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);//kullanıcı var mı, kontrol et.
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
