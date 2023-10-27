using ExeedTask.Models;
using ExeedTask.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExeedTask.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> UserManager;
		private readonly SignInManager<ApplicationUser> SignInManager;
        public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser> _signInManager)
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
        }

        

        public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel newUser)
		{
			if (ModelState.IsValid)
			{
				ApplicationUser userModel = new ApplicationUser();
				userModel.UserName = newUser.UserName;	
				userModel.Email = newUser.Email;
				userModel.PasswordHash = newUser.Password;
				IdentityResult result =  await UserManager.CreateAsync(userModel,newUser.Password);

				if (result.Succeeded)
				{
					await UserManager.AddToRoleAsync(userModel,"NormalUser");
					await SignInManager.SignInAsync(userModel, isPersistent: false);
					return RedirectToAction("GetAllProducts","Product");
				}
				else
				{
                    foreach (var item in result.Errors)
                    {
						ModelState.AddModelError("",item.Description);
                    }
                }
				
			}
			return View(newUser);
		}
		public IActionResult LogIn()
		{
			return View();
		}
		[HttpPost]
		
		public async Task<IActionResult> LogIn(LoginViewModel userVm)
		{
			if (ModelState.IsValid)
			{
				 ApplicationUser userDb = await UserManager.FindByNameAsync(userVm.UserName);
				if (userDb != null)
				{
					bool found = await UserManager.CheckPasswordAsync(userDb, userVm.Password);
					if (found)
					{
						await SignInManager.SignInAsync(userDb, isPersistent: userVm.RememberMe);
						return RedirectToAction("index", "Home");
					}
                    ModelState.AddModelError("", "Wrong UserName or Password");
                }
				ModelState.AddModelError("","Wrong UserName or Password");
			}
			return View(userVm);
		}
		public async Task<IActionResult> SignOut()
		{
			await SignInManager.SignOutAsync();
			return RedirectToAction("Index","Home");
		}

        public async Task<IActionResult> RegisterAdmin(RegisterViewModel newUser)
		{
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.UserName = newUser.UserName;
                userModel.Email = newUser.Email;
                userModel.PasswordHash = newUser.Password;
                IdentityResult result = await UserManager.CreateAsync(userModel, newUser.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(userModel, "Admin");
                    //await SignInManager.SignInAsync(userModel, isPersistent: false);
                    return RedirectToAction("GetAllProducts", "Product");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return View(newUser);
        }

		public IActionResult AccessDenied()
		{
			return View();
		}

    }
}
