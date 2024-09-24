using BouillonChanvre.Data;
using BouillonChanvre.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BouillonChanvre.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch subcategories based on CategoryID
        public async Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId)
        {
            return await _context.SubCategories
                .Where(sc => sc.CategoryID == categoryId)  // Filter by CategoryID
                .ToListAsync();
        }

        // Create a new subcategory and link it to a category
        public async Task CreateSubCategory(string subCategoryName, int categoryId)
        {
            Console.WriteLine($"CATEGORY ID: {categoryId}");
            var subCategory = new SubCategory { Name = subCategoryName, CategoryID = categoryId };
            _context.SubCategories.Add(subCategory);
            await _context.SaveChangesAsync();
        }
    }

}
