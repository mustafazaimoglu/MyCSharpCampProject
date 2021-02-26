﻿using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    // RESTFUL --> HTTP /* TCP(Wired) */ --> 
    // Controllers istekleri barındırır
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService) // Entity Manager kendisi hariç başka dalı enjekte edemez.
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        // AOP loglama
        // [LogAspect] --> AOP
        // [Validate]
        // [RemoveCache]
        // [Transaction]
        // [Performance]


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // Bussiness Codes
            // Validation

            //if(product.UnitPrice <= 0) // FLUENTVALIDATION
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}

            //if(product.ProductName.Length < 2)
            //{
            //    // Magic Strings
            //    return new ErrorResult(Messages.ProductNameInvalid );
            //}

            //ValidationTool.Validate(new ProductValidator(),product); // old type


            IResult result = BusinessRules.Run(CheckIfCategoryCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExist(product.ProductName),
                CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
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

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);
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

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(), Messages.ProductListed);

        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfCategoryCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();


            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

    }
}
