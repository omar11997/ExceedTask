using ExeedTask.Models;

namespace ExeedTask.Reposatory
{
    public class ProductRepo : IProductRepo
    {
        Context Context;
        public ProductRepo(Context context)
		{ 
            this.Context = context;
        }

		public void AddProduct(Product product)
		{
			Context.Products.Add(product);
			Context.SaveChanges();	
			
		}

		public void DeleteProduct(Product product)
		{
			Context.Products.Remove(product);
			Context.SaveChanges();
		}

		public List<Product> GetAllProducts()
        {
            return Context.Products.ToList();
        }

		public Product GetProductById(Guid id)
		{
            return Context.Products.SingleOrDefault(e => e.Id == id);
		}

		public List<Product> GetProductsByCategory(int id)
		{
			return Context.Products.Where(e => e.CategoryId ==id).ToList();	
		}

		public void UpdateProduct(Product product)
		{
			Context.Products.Update(product);
			Context.SaveChanges();
		}
	}
}
