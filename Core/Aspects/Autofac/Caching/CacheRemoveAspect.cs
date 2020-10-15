using Core.CrossCuttingConcerns.Caching;
using Core.IoC;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern) //bu CacheRemove işlemi yeni ürün elkelendiğinde, güncellendiğinde,silindiğinde
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)//crud işlemi yaptığımızda başarılı işlem gerçekleştirdiğinde 
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
