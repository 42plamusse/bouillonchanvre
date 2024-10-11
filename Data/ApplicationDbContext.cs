using BouillonChanvre.Models;
using Microsoft.EntityFrameworkCore;

namespace BouillonChanvre.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }

        // Override the OnModelCreating method to define custom model configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the foreign key relationship between SubCategory and Category with restricted delete behavior
            modelBuilder.Entity<SubCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(sc => sc.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete

            base.OnModelCreating(modelBuilder);  // Apply base configurations
        }
    }
}
