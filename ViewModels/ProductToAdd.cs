using System.ComponentModel.DataAnnotations;

namespace ExeedTask.ViewModels
{
	public class ProductToAdd
	{
		public string Name { get; set; }
		[Required]
		public int Price { get; set; }
		public DateTime StartDate { get; set; }

		public int DurationInDays { get; set; }

		public int CategoryId { get; set; }
	}
}
