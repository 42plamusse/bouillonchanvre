using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BouillonChanvre.Data;
using BouillonChanvre.Models;

namespace BouillonChanvre.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Common query logic for fetching products with related entities
        private IQueryable<Product> GetProductQuery()
        {
            return _context.Products
                .Include(p => p.Category)
                .Include(p => p.Subcategory)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Sizes)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Images)
                .Include(p => p.Variants)
                    .ThenInclude(v => v.Description);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await GetProductQuery().ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await GetProductQuery()
                .FirstOrDefaultAsync(p => p.ProductID == productId);
        }

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await GetProductQuery()
                .FirstOrDefaultAsync(p => p.ProductID == productId);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
