using Castle.DynamicProxy;
using Core.Utilities.Intercepters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute//bir metodu nasıl yorumlacağını söyler
    {
        //invocation : çalıştırılmaya çalışılan methodumuzdur
        protected virtual void OnBefore(IInvocation invocation)//method çalışmadan önce sen çalış
        {

        }
        protected virtual void OnAfter(IInvocation invocation)
        {

        }
        protected virtual void OnException(IInvocation invocation)
        {

        }
        protected virtual void OnSuccess(IInvocation invocation)
        {

        }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();//aspect i çalıştır
            }
            catch (Exception)
            {
                isSuccess = false;
                OnException(invocation); //hata aspect i çalıştır
                throw;
            }
            finally
            {
                if (isSuccess) //aspect başarılı ile 
                {
                    OnSuccess(invocation);
                }
            }
            OnAfter(invocation);
            
        }
    }
}
