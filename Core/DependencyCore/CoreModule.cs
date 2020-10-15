using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyCore
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddMemoryCache();//normalde bunu startup da eklemiştik ama bunu burada merkezileştirdik

            //burada bir extention yazacağız, IServiceCollection u extend edecek

            //sisteme ekle ilk olarak
            //birisi senden ICacheManager isterse,ona MemoryCacheManager ver - redix e geçilirse değiştirilip redisx yapılır.
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }
    }
}
