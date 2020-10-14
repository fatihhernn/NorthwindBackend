using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHelper//bugün jwt ile yaparım ertesi gün başka tokenla yaparım....
    {
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);//benim user bilgime göre token üretecek
    }
}
