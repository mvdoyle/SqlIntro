using System.Collections.Generic;

namespace SqlIntro
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        void DeleteProduct(int id);
        void InsertProduct(Product prod);
        void UpdateProduct(Product prod);
    }
}