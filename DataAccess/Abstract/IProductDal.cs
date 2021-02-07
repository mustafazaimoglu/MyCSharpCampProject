using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product> // Dal = Data Access Layer or Dao = Data Access Object
    {
        List<ProductDetailDto> GetProductDetails();
    }
}

// Code refactoring