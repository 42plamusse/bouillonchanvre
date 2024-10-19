using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BouillonChanvre.Data;
using BouillonChanvre.Models;

namespace BouillonChanvre.Services
{
    public class ProductService : IProductService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IBlobStorageService _blobStorageService;

        public ProductService(IDbContextFactory<ApplicationDbContext> contextFactory, IBlobStorageService blobStorageService)
        {
            _contextFactory = contextFactory;
            _blobStorageService = blobStorageService;
        }

        private IQueryable<Product> GetProductQuery(ApplicationDbContext context)
        {
            return context.Products
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Images)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Description);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using var context = _contextFactory.CreateDbContext();
            return await GetProductQuery(context).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await GetProductQuery(context)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductID == productId);
        }

        public async Task CreateProduct(Product product)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            using var context = _contextFactory.CreateDbContext();
            product.Category = null;
            product.Subcategory = null;
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            using var context = _contextFactory.CreateDbContext();
            var product = await GetProductQuery(context)
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public bool ValidateProduct(Product product, out List<ValidationResult> validationResults)
        {
            validationResults = new List<ValidationResult>();
            var context = new ValidationContext(product);

            bool isValid = Validator.TryValidateObject(product, context, validationResults, true);

            foreach (var variant in product.Variants)
            {
                var variantContext = new ValidationContext(variant);
                isValid = Validator.TryValidateObject(variant, variantContext, validationResults, true) && isValid;
            }

            return isValid;
        }

        public async Task DeleteProductVariant(int productVariantId)
        {
            using var context = _contextFactory.CreateDbContext();
            var productVariant = await context.ProductVariants
                .FirstOrDefaultAsync(v => v.ProductVariantID == productVariantId);

            if (productVariant != null)
            {
                context.ProductVariants.Remove(productVariant);
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteProductVariantImage(int productImageId)
        {
            using var context = _contextFactory.CreateDbContext();

            // Find the product image by its ID
            var productImage = await context.ProductImages
                .FirstOrDefaultAsync(img => img.ProductImageID == productImageId);

            if (productImage != null)
            {
                // Remove the image from the context
                context.ProductImages.Remove(productImage);

                // Optionally: Remove the image from storage if necessary
                await _blobStorageService.DeleteFileAsync("product-images", productImage.ImageUrl);

                // Save changes to the database
                await context.SaveChangesAsync();
            }
        }

    }
}
