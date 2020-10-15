using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.IoC
{
    public static class ServiceTool
    {
        //service provider ulaş
        public static IServiceProvider ServiceProvider { get; set; }
        public static IServiceCollection Create(IServiceCollection services)//.net core a ait olan service collection enjecte edilir
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;//.net core un kendi servislerine erişebiliyorum.
        }
    }
}
