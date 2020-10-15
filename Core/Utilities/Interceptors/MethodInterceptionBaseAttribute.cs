
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Intercepters
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true  ,Inherited =true)]//atribute kısatlaması koyalım
    public abstract class MethodInterceptionBaseAttribute:Attribute,IInterceptor
    {
        public int Priorty { get; set; }

        public virtual void Intercept(IInvocation invocation)//bu kodun değiştirilmesini sağlamak için VIRTUAL konulması tavsiye edilir
        {
            
        }
    }
}
