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

            Product product = null;

            foreach (var prod in repo.GetProductsWithReview())
            {
                if (product == null)
                {
                    product = prod;
                }
                Console.WriteLine($"Product Name: {prod.Name} {prod.Rating}");
            }

            //TODO: Refactor testing for database functionality
            /*
            product.Name = "Cody's Lame Product";
            repo.UpdateProduct(product);
            repo.DeleteProduct(3);
            if (product != null)
            {
                product.Name
            }
            */

            Console.ReadLine();
        }


    }
}
