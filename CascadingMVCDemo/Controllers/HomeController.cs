using CascadingMVCDemo.Data;
using CascadingMVCDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CascadingMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //Get all categories
            var categories = _context.Categories.ToList();

            //Get List of all Products (list of product depend on category) so we use this code
            //rather then populate 
            var products = new List<Product>();


            //on categories and products we add one more entry
            //categories.Add(new Category()
            //{
            //    Id = 0,
            //    CategoryName = "--Select Category--"

            //});

            //products.Add(new Product()
            //{
            //    Id = 0,
            //    Name = "--Select Product--"

            //});



            //so we pass both above list to our view
            //we can use viewModel (VM) but here we use viewBag to understand the concepts
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.Products = new SelectList(products, "Id", "Name");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //get product bt category id
        public JsonResult GetProductByCategoryId(int categoryId)
        {
            //get product from database base on category id
            return Json(_context.Products.Where(u=>u.CategoryId== categoryId).ToList());
        }
    }
}
