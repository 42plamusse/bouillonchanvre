using System.ComponentModel.DataAnnotations;

namespace BouillonChanvre.Models {
    public class Recipe {
        public int Id { get; set; }

        [Required]
        [StringLength (255)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Ingredients { get; set; }

        [Required]
        public required string Instructions { get; set; }

        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int Servings { get; set; }

        [StringLength (50)]
        public required string Difficulty { get; set; }

        public required string ImageUrl { get; set; }

            // Ensure the DateAdded property is always in UTC
        private DateTime _dateAdded;
        public DateTime DateAdded
        {
            get => _dateAdded;
            set => _dateAdded = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public Recipe()
        {
            DateAdded = DateTime.UtcNow;
        }   
    }
}