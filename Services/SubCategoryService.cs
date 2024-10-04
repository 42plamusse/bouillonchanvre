using BouillonChanvre.Data;
using BouillonChanvre.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BouillonChanvre.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public SubCategoryService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.SubCategories
                .Where(sc => sc.CategoryID == categoryId)
                .ToListAsync();
        }

        public async Task CreateSubCategory(string subCategoryName, int categoryId)
        {
            using var context = _contextFactory.CreateDbContext();
            var subCategory = new SubCategory { Name = subCategoryName, CategoryID = categoryId };
            context.SubCategories.Add(subCategory);
            await context.SaveChangesAsync();
        }
    }
}
