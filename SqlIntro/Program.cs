using System;
using System.Configuration;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            var repo = new DapperProductRepository(connectionString);
            var productRepo = new ProductRepository(connectionString);

            Product product = null;
            
            //Test for only products with a review
            //foreach (var prod in repo.GetProductsWithReview())
            //{
            //    if (product == null)
            //    {
            //        product = prod;
            //    }
            //    Console.WriteLine($"Product Name: {prod.Name} {prod.Rating}");
            //}

            //Test for products that may or may not have a review
            foreach (var prod in repo.GetProductsAndReview())
            {
                if (product == null)
                {
                    product = prod;
                }

                if (prod.Rating == null)
                {
                    prod.Rating = 0;
                }

                Console.WriteLine($"Product Name: {prod.Name}, Product Rating: {prod.Rating}");
            }
            
            Console.ReadLine();
        }
    }
}
