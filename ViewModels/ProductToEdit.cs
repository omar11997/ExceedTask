using System.ComponentModel.DataAnnotations;

namespace ExeedTask.ViewModels
{
	public class ProductToEdit
	{
		public string Name { get; set; }
		[Required]
		public int Price { get; set; }

		
		public DateTime StartDate { get; set; }

		public int DurationInDays { get; set; }
	}
}
