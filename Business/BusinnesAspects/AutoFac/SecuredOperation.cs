using Castle.DynamicProxy;
using Core.IoC;
using Core.Utilities.Interceptors;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Concrete.Constants;

namespace Business.BusinnesAspects.AutoFac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');//att array yazamadığımıdan burada virgül ile ayırıp arraya atıyoruz.
            _httpContextAccessor= ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//rollere de sahibim
            foreach (var role in roleClaims)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
