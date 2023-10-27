using System.Security.Claims;
using ExeedTask.Models;
using ExeedTask.Reposatory;
using ExeedTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExeedTask.Controllers
{
    public class ProductController : Controller
    {
        IProductRepo ProductReposatory;
        ICategory CategoryReposatory;

        public ProductController(IProductRepo productRepo,ICategory categoryRepo)
        {
           ProductReposatory = productRepo;
           CategoryReposatory = categoryRepo;
        }
        [Authorize(Roles = "Admin,NormalUser")]
        public IActionResult GetAllProducts()
        {
			ViewBag.Categories = CategoryReposatory.GetAllCategories();
			List<Product> products = ProductReposatory.GetAllProducts();
            if (User.IsInRole("Admin"))
            {
                return View(products);
            }
            else
            {
                List<Product> productsToShow = new List<Product>();
                DateTime currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                foreach (Product product in products)
                {
                    DateTime productStartDate = new DateTime(product.StartDate.Year, product.StartDate.Month, product.StartDate.Day);
                    TimeSpan differenceInDays = currentDate - productStartDate;
                    if (differenceInDays.Days > 0  && differenceInDays.Days <= product.DurationInDays)
                    {
                        productsToShow.Add(product);
                    }

                }
                return View(productsToShow);
            }
            
        }
        [Authorize(Roles = "Admin,NormalUser")]
        public IActionResult ProductDetails(Guid id) 
        {
            Product product = ProductReposatory.GetProductById(id);
            if (product == null)
            {
                return View("Product Not Found");
            }
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult NewProduct()
		{
            ViewBag.Categories = CategoryReposatory.GetAllCategories();
			return View();
		}
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(ProductToAdd productToAdd)
        {
            Product newProduct = new Product();

            var claimsId = User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);
            
            newProduct.Name = productToAdd.Name;
            newProduct.Price = productToAdd.Price;
            newProduct.StartDate = new DateOnly(productToAdd.StartDate.Year, productToAdd.StartDate.Month, productToAdd.StartDate.Day); //productToAdd.StartDate;
            newProduct.DurationInDays = productToAdd.DurationInDays;
            newProduct.CategoryId = productToAdd.CategoryId;
            newProduct.UserId = claimsId.Value;
            ProductReposatory.AddProduct(newProduct);
            return RedirectToAction("GetAllProducts");

        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProduct(Guid id)
        {
            Product productToDelete = ProductReposatory.GetProductById(id);
            ProductReposatory.DeleteProduct(productToDelete);
            return RedirectToAction("GetAllProducts");
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(Guid id)
        {
            
            ViewBag.Categories = CategoryReposatory.GetAllCategories();
            Product product = ProductReposatory.GetProductById(id);
            
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateProduct(Product product)
        {
            var claimsId = User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);
            product.UserId = claimsId.Value;
            ProductReposatory.UpdateProduct(product);
            return RedirectToAction("GetAllProducts");
        }

        public IActionResult GetProductOfCategory(int id )
        {
			ViewBag.Categories = CategoryReposatory.GetAllCategories();
            ViewBag.CategoryId = id;
			List<Product> products = ProductReposatory.GetProductsByCategory(id);
            return View(products);
        }
    }
}
