using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var p in productManager.GetByUnitPrice(40,150))
            {
                Console.WriteLine(p.ProductName);
            }

        }
    }
}
