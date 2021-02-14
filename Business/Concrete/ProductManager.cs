﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    // RESTFUL --> HTTP /* TCP(Wired) */ --> 
    // Controllers istekleri barındırır
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        // AOP loglama
        // [LogAspect] --> AOP
        // [Validate]
        // [RemoveCache]
        // [Transaction]
        // [Performance]
        public IResult Add(Product product)
        {
            // Bussiness Codes

            if(product.ProductName.Length < 2)
            {
                // Magic Strings
                return new ErrorResult(Messages.ProductNameInvalid );
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        // [Cache]
        public IDataResult<List<Product>> GetAll()
        {
            // İş Kodları

            if (DateTime.Now.Hour == 2)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 02) // Sistem bakımda yazımı için
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),Messages.ProductListed);
            
        }
    }
}
