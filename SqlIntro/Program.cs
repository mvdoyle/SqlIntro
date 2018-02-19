using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            var connection = new MySqlConnection(connectionString);
            var repo = new ProductRepository(connection);

            Product product = null;
            
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
