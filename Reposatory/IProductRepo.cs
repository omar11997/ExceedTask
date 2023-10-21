using ExeedTask.Models;

namespace ExeedTask.Reposatory
{
    public interface IProductRepo
    {
        List<Product> GetAllProducts();
        Product GetProductById(Guid id);
        List<Product> GetProductsByCategory(int id);
        void DeleteProduct(Product product);

        void AddProduct(Product product);

        void UpdateProduct(Product product);    
    }
}
