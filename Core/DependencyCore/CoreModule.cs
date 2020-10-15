using Core.IoC;
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
        }
    }
}
