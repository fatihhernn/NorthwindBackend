using Business.Abstract;
using Business.Concrete.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

       

        public IDataResult<Product> GetById(int productId)
        {
            //bu method bağımlıdır....sistem değiştirmek zorlaşır.... bağımlılık soyutlaştırılmalı... Loose Coupling – Gevşek Bağlılık Prensibi 
            //EfProductDal productDal = new EfProductDal();
            //return productDal.Get(p=>p.ProductID==productId)
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductID == productId)); /*_productDal.Get(p => p.ProductID == productId);*/
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList()); // _productDal.GetList().ToList();
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList()); //_productDal.GetList(p=>p.CategoryId==categoryId).ToList();
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            //return new SuccessResult("Ürün başarıyla güncellendi");   MAGIC STRINGTEN KURTULALIM, CONSTANT KLASÖRÜNDE EKLEDİM
            return new SuccessResult(Messages.ProductUpdated);
        }
        public IResult Add(Product product)
        {
            //business codeları yazılabilir, validasyon işlemleri..
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
    }
}
