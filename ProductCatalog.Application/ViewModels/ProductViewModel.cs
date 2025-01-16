using Microsoft.AspNetCore.Http;
using ProductCatalog.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Application.ViewModels
{

    public class ProductViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile? ImageFile { get; set; }
        public int CategoryId { get; set; }

    public static Product MapToEntity(ProductViewModel viewModel)
        {
            return new Product
            {
                Id = viewModel.Id ?? 0,
                Name = viewModel.Name,
                CreationDate = viewModel.CreationDate ?? DateTime.MinValue,
                CreatedByUserId = viewModel.CreatedByUserId,
                StartDate = viewModel.StartDate,
                Duration = viewModel.Duration,
                Price = viewModel.Price,
                ImagePath = viewModel.ImagePath ?? "c://ibrahim",
                CategoryId = viewModel.CategoryId
            };
        }

    }
}
