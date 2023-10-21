using System.ComponentModel.DataAnnotations;

using Microsoft.Build.Framework;

namespace ExeedTask.ViewModels
{
    public class LoginViewModel
    {
        
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set;}
    }
}
