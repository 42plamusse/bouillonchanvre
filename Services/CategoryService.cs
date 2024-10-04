using BouillonChanvre.Data;
using BouillonChanvre.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BouillonChanvre.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public CategoryService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Categories.ToListAsync();
        }

        public async Task CreateCategory(string categoryName)
        {
            using var context = _contextFactory.CreateDbContext();
            var category = new Category { Name = categoryName };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }
    }
}
