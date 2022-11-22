using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data
{
    public partial class Game
    {
        public Game()
        {
            GameImages = new HashSet<GameImage>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            WishLists = new HashSet<WishList>();
            Categories = new HashSet<Category>();
            GameFeatures = new HashSet<GameFeature>();
            Platforms = new HashSet<Platform>();
        }

        public int GameId { get; set; }
        [Required(ErrorMessage = "Please enter a game name.")]
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public decimal? Price { get; set; }
        [Required(ErrorMessage = "Quantity field is required.")]
        [Range(0, 100000, ErrorMessage = "Quantity must be more than 0.")]
        public int? Pquantity { get; set; }

        [Required(ErrorMessage = "Please enter a game description.")]
        [MaxLength(255)]
        public string? Description { get; set; }
        public int? Discount { get; set; }
        [Required(ErrorMessage = "Please enter a release date.")]
        public DateTime? ReleaseDate { get; set; }
        [Required(ErrorMessage = "Please select a game studio.")]
        public int? GameStudio { get; set; }
        public bool? Digital { get; set; }

        public virtual ICollection<PlatformGame> PlatformGames { get; set; }
        public virtual ICollection<GameFeatureGame> GameFeatureGames { get; set; }
        public virtual ICollection<GameCategory> GameCategories { get; set; }

        public virtual ICollection<GameImage> GameImages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<GameFeature> GameFeatures { get; set; }
        public virtual ICollection<Platform> Platforms { get; set; }
    }
}
