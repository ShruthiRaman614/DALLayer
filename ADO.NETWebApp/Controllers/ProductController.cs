using Microsoft.AspNetCore.Mvc;
using DAL;

namespace ADO.NETWebApp.Controllers
{
    public class ProductController : Controller
    {

        DataConnection _connection;
        public ProductController(DataConnection connection)
        {
           _connection = connection;
        }
       
        public IActionResult Index()
        {
            var product = _connection.Getproducts();
            

            return View(product);
            
        }


        public IActionResult Create()
        {
            ViewBag.Categories = _connection.GetCategories();
            return View();
        }

[HttpPost]
        public IActionResult Create(Product p)
        {
            ViewBag.Categories = _connection.InsertProducts(p);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.Categories = _connection.GetCategories();
            return View();
        }

        public IActionResult Edit(int id)
        {
            var product = _connection.GetProducts(id);
            ViewBag.Categories = _connection.GetCategories();
            return View("Create", product);

        }
        public IActionResult Delete(int id)
        {
            _connection.DeleteProducts(id);
            if(ModelState.IsValid)
            ViewBag.Categories = _connection.GetCategories();
            var product = _connection.Getproducts();

            return View("Index",product);
        }
        
    }
}
