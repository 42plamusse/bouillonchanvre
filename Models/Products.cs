using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BouillonChanvre.Models
{
    public class Category
    {
        public int CategoryID { get; set; }  // Unique identifier

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Category name (e.g., "Huile Infusées", "Condiments", "Miels")
        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();  // List of subcategories
        public ICollection<Product> Products { get; set; } = new List<Product>();  // List of products under this category
    }

    public class SubCategory
    {
        public int SubCategoryID { get; set; }  // Unique identifier for the subcategory

        [Required]
        [StringLength(100)]
        public string Name { get; set; }  // Name of the subcategory (e.g., "Huile d'olive", "Sauce", "Hydro-Miel de lavande")

        // Foreign Key to Category
        public int CategoryID { get; set; }  // Foreign key to the category
        public virtual Category Category { get; set; }  // Navigation property to the category

        public ICollection<Product> Products { get; set; } = new List<Product>();  // Products of this subcategory
    }


    public class Product
    {
        public int ProductID { get; set; }  // Unique identifier for the product

        [Required]
        [StringLength(500)]
        public string ProductName { get; set; }  // Name of the product (e.g., "OLI", "SUPRA", "PIPA")

        public int? SubCategoryID { get; set; }  // Foreign key to the product subcategory
        public virtual SubCategory Subcategory { get; set; }  // Navigation property to the product subcategory

        [MinLength(1, ErrorMessage = "Un produit doit contenir au moins une variante.")]
        public ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();  // List of variants for this product (can be just 1)

        public int? CategoryID { get; set; }  // Foreign key to the category
        public virtual Category Category { get; set; }  // Navigation property to the category
    }

    public class ProductVariant
    {
        public int ProductVariantID { get; set; }  // Unique identifier for the variant

        [StringLength(100)]
        public string? VariantName { get; set; }  // Name of the variant (optional, e.g., "Argent", "Blanche", "Basilic & Yuzu")

        [MinLength(1, ErrorMessage = "Une variante doit contenir au moins une taille.")]
        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();  // Multiple sizes for the product or variant

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();  // Collection of images for the product or variant

        public ProductDescription Description { get; set; }  // Detailed description for this specific product or variant

        public int ProductID { get; set; }  // Foreign key to the product
        public virtual Product Product { get; set; }  // Navigation property to the product
    }

    public class ProductSize
    {
        public int ProductSizeID { get; set; }  // Unique identifier for the size

        [Required]
        [StringLength(500)]
        public string Reference { get; set; }

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Size { get; set; }  // Size of the product or variant (e.g., 100ml, 250ml)

        public SizeUnit SizeUnit { get; set; }  // Unit of measurement for size (e.g., "ml", "gr")

        [StringLength(300)]
        public string? CBDConcentration { get; set; }  // CBD concentration in mg or percentage

        [Required]
        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }  // Price for this specific size

        public int ProductVariantID { get; set; }  // Foreign key to the product variant
        public virtual ProductVariant ProductVariant { get; set; }  // Navigation property to the product variant
    }

    public class ProductImage
    {
        public int ProductImageID { get; set; }  // Unique identifier for the image

        [Required]
        public string ImageUrl { get; set; }  // URL or path to the image

        public int ProductVariantID { get; set; }  // Foreign key to the product variant
        public virtual ProductVariant ProductVariant { get; set; }  // Navigation property to the product variant
    }

    public class ProductDescription
    {
        public int ProductDescriptionID { get; set; }  // Unique identifier for the description

        [StringLength(500)]
        public string? Subtitle { get; set; }

        [StringLength(500)]
        public string? Slogan { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? Ingredients { get; set; }  // Ingredients list

        [StringLength(2000)]
        public string? Benefits { get; set; }  // Benefits or therapeutic effects

        [StringLength(2000)]
        public string? UsageInstructions { get; set; }  // Usage instructions or recipe suggestions

        [StringLength(2000)]
        public string? ProducerInformation { get; set; }  // Information about the producer (e.g., apiculture details)

        public int ProductVariantID { get; set; }  // Foreign key to the product variant
        public virtual ProductVariant ProductVariant { get; set; }  // Navigation property to the product variant
    }
}

