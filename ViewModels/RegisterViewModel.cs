using System.ComponentModel.DataAnnotations;

namespace ExeedTask.ViewModels
{
	public class RegisterViewModel
	{
		public string UserName { get; set; }
		[RegularExpression("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$",ErrorMessage ="Ivalid Email")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		
	}
}
