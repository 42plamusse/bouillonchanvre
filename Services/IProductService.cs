using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BouillonChanvre.Models;

namespace BouillonChanvre.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();  // Get all products with their variants and sizes
        Task<Product> GetProductById(int productId);               // Get a single product by ID
        Task CreateProduct(Product product);                       // Create a new product
        Task UpdateProduct(Product product);                       // Update an existing product
        Task DeleteProduct(int productId);                         // Delete a product by ID
        bool ValidateProduct(Product product, out List<ValidationResult> validationResults);
    }
}
