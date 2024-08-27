using BouillonChanvre.Models;
using Microsoft.EntityFrameworkCore;

namespace BouillonChanvre.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

        public DbSet<Recipe> Recipes { get; set; }
    }
}