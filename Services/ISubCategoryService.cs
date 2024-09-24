using BouillonChanvre.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BouillonChanvre.Services
{
    public interface ISubCategoryService
    {
        Task<List<SubCategory>> GetSubCategoriesByCategoryId(int categoryId);
        Task CreateSubCategory(string subCategoryName, int categoryId);
    }
}
