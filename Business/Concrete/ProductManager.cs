using Business.Abstract;
using Business.Concrete.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [CacheAspect(1)]
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

        [ValidationAspect(typeof(ProductValidator),Priorty =1)]
        public IResult Add(Product product)
        {
            //bu kodu CORE katmanında merkezileştirp her yerde onu kullancam
            //ProductValidator productValidationRules = new ProductValidator();
            //var result = productValidationRules.Validate(product);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}
            //ValidationTools.Validate(new ProductValidator(), product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }


        //transaction işlemi için test alanı
        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            //update çalışsın ama product validationda hata çıksın ve transaction geri alsın
            //aşağıda yazılan 2 işlem invocationlarımızdır
            _productDal.Update(product);
            _productDal.Add(product);
            //update işlemini sıkıntısız yapacak fakat ekleme işleminde patladığından dolayı veritabanında update işlemin yapmaz, transaction işlemi tanımlı olduğundan dolayı
            //_productDal.Add(product); olmasaydı UPDATE işlemini yapıldığını görebilirz
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
