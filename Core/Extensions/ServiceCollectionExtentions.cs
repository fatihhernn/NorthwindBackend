using Core.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ServiceCollectionExtentions//genel olarak extentionları static yapalım
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)//.net core, api tarafında merkezi koleksiyonlarımızı eklemiş olacağız
        {
            foreach (var module in modules)
            {
                module.Load(services);//bu modülleri .net core eklemiş olduk
            }
            return ServiceTool.Create(services);//ServiceTool :  servislerimizi yapılandıran operasyon utilities dir       
        }

        //son olarak ,bunu api tarafina ekleyip ve işi bitirelim. yani startup a
    }
}
