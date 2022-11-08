using System;
using System.Collections.Generic;

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
        public string Name { get; set; } = null!;
        public decimal? Price { get; set; }
        public int? Pquantity { get; set; }
        public string? Description { get; set; }
        public int? Discount { get; set; }
        public DateTime? ReleaseDate { get; set; }
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
