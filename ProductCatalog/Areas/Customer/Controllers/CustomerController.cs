using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.Application.IServices;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerController : Controller
    {
        private readonly IProductService _productService;

        public CustomerController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var products = await _productService.GetProductsAsync(); 
            
            var categories = await _productService.GetProductCategoriesAsync(); 
            
            ViewBag.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(); 
            
            if (categoryId.HasValue) 
            { 
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList(); 
            }
            
            return View(products);
        }
    }
}
