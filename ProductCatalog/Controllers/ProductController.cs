using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProductCatalog.Application.IServices;
using ProductCatalog.Application.ViewModels;
using ProductCatalog.Core.Entities;
using System.Security.Claims;

namespace ProductCatalog.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;

        public ProductController(IProductService productService, IFileService fileService)
        {
            _productService = productService;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync(User?.IsInRole("Admin") ?? false);
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Duration = product.Duration,
                StartDate = product.StartDate,
                ImagePath = product.ImagePath,
                CreatedByUserId = product.CreatedByUserId
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _productService.GetProductCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _productService.GetProductCategoriesAsync();
                return View(product);
            }

            if (product.ImageFile != null)
            {
                try
                {
                    product.ImagePath = await _fileService.SaveFileAsync(product.ImageFile, "images");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while uploading the file: " + ex.Message);
                    return View(product);
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Please upload an image.");
                ViewBag.Categories = await _productService.GetProductCategoriesAsync();
                return View(product);
            }

            product.CreatedByUserId = User?.FindFirstValue(ClaimTypes.NameIdentifier);

            await _productService.AddProductAsync(product);

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            ViewBag.Categories = await _productService.GetProductCategoriesAsync();
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = await _productService.GetProductCategoriesAsync();
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
