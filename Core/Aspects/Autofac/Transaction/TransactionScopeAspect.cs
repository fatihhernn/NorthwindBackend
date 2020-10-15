using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception //burada Intercept metodunu override edelim
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope=new TransactionScope())//transaction yazmak için TRANSACTIONSCOPE devreye sokmamız gerekiyor . disposable pattern yapısına uygun olması için using bloğuna alırız
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
