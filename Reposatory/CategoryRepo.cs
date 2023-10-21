using ExeedTask.Models;

namespace ExeedTask.Reposatory
{
	public class CategoryRepo : ICategory
	{
		Context Context;
		public CategoryRepo(Context context) 
		{
			Context = context;
		}
		public List<Category> GetAllCategories()
		{
			return Context.Categories.ToList();
		}
	}
}
