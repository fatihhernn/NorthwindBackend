using Core.Entity.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);//sisteme kullanıcı eklemek istiyorum.
        User GetByEmail(string email); //mail vasıtasıyla bulacağız
    }
}
