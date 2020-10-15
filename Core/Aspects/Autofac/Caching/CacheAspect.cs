using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.IoC;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
     public class CacheAspect:MethodInterception
    {
        private int _duration;//kullanıcıdan istiyor alcağız
        private ICacheManager _cacheManager;//memorycache manager mı, redix mi ? hangisi olduğunu burada vercez

        public CacheAspect(int duration=60)//default olarak 60 yazalım. istenirse girilebilir.
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();//ICacheManager :  memory cache gelecek buraya
        }
        public override void Intercept(IInvocation invocation)//key değeri olarak metod ismi ve parametre şeklinde olacak .. product.GetByCategory(1,sfsdas)
        {
            var metotName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");//product.getall
            var arguments = invocation.Arguments.ToList();
            var key = $"{metotName}({string.Join(",",arguments.Select(x=>x?.ToString()??"<Null>"))})";//operasyonun içeriği bazlı bir caching işlemi gerçekleştirmeye çalışıyoruz
            if (_cacheManager.IsAdd(key))//bu key daha önce eklenmiş ise , metodu hiç çalıştırma o metoda ait return value döndür
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
