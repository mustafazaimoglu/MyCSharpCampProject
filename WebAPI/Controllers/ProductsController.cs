﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    // To reach this site we have to say the word before the contoller...
    [Route("api/[controller]")]
    [ApiController] // Attribute (for java = annotation)
    public class ProductsController : ControllerBase
    {
        // Loosly coupled -- Gevsek Bağlılık
        IProductService _productService; // this is a field not a property and _(underscore) is a naming convention
        // IoC Container = Inversion of Control 

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            // Swagger
            // Dependeny Chain -- 
            var result = _productService.GetAll();

            if (result.Success)
            {
                return Ok(result);
                // 200 Başarılı
            }

            return BadRequest(result);
            // 400 Bad Request
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);

            if(result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Product p)
        {
            var result = _productService.Add(p);

            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}