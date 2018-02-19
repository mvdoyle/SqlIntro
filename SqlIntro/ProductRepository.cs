using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SqlIntro
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var conn = _conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select Name from product";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString(), Id = (int)dr["Id"] };
                }
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "delete from product where productid = @id";
                cmd.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            //This is annoying and unnecessarily tedious for large objects.
            //More on this in the future...  Nothing to do here..
            using (var conn = _conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE product SET name = @name WHERE ProductId = @id";
                cmd.AddWithValue("@name", prod.Name);
                cmd.AddWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = _conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT into product (name) values(@name)";
                cmd.AddWithValue("@name", prod.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetProductsWithReview()
        {
            using (var conn = _conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.Name, pr.Rating FROM product AS p INNER JOIN productreview pr ON pr.ProductID = p.ProductID;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product { Name = dr["Name"].ToString(), Rating = dr["Rating"] as int? ?? 0 };
                }
            }
        }
        /// <summary>
        /// Reads all the products from the products table and the reviews, regardless of whether they have a review
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProductsAndReview()
        {
            using (var conn = _conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT p.Name, pr.Rating FROM product AS p LEFT JOIN productreview pr ON pr.ProductID = p.ProductID;";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product {Name = dr["Name"].ToString(), Rating = dr["Rating"] as int? ?? 0 };
                }
            }
        }  
    }
}
