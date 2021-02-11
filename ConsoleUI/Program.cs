using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Data Transformation Object

            ProductTest();

            // IoC

            //CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var c in categoryManager.GetAll())
            {
                Console.WriteLine(c.CategoryName);
            }


        }
          
        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetails();

            if(result.Success)
            {
                foreach (var p in result.Data)
                {
                    Console.WriteLine(p.ProductName + " / " + p.CategoryName);
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }

            
        }
    }
}
