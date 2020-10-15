using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //dependency configurasyonları burada yapıldı..

            //product manager register et, iproductservice olarak kaydet
            //eğer birisi contructorda  iproductservice isterse biz ona product manager vercez
            //business iproductdal istiyordu,
            builder.RegisterType<ProductManager>().As<IProductService>();
            //eğer birisi senden productdal isterse iproductdal ver
            builder.RegisterType<EfProductDal>().As<IProductDal>();


            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //attribute istenirse
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();//bir tek instance oluşssun her sefende çok instance oluşmasın



        }
    }
}
