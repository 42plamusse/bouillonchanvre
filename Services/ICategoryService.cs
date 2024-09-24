using BouillonChanvre.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BouillonChanvre.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task CreateCategory(string categoryName);
    }
}
