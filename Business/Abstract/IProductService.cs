using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {

        //Product GetById(int productId);
        //List<Product> GetList();//ürünleri listeleyecek bir metot, expression geçmiyoruz.. service katmanında tamamlamamız gerekiyor, teknoloji destekleri olmayabilir.. arayüzde saf metotlarla çalışırız..
        IDataResult<Product> GetById(int productId);      
        IDataResult<List<Product>> GetList();
        IDataResult<List<Product>> GetListByCategory(int categoryId);//tek bir kategoriye getir dediğimizde
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);

        IResult TransactionalOperation(Product product);
    }
}
