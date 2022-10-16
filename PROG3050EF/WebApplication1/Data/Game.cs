using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("Game")]
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

        [Key]
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("price", TypeName = "decimal(19, 2)")]
        public decimal? Price { get; set; }
        public int? Pquantity { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("discount")]
        public int? Discount { get; set; }
        [Column("releaseDate", TypeName = "datetime")]
        public DateTime? ReleaseDate { get; set; }
        [Column("gameStudio")]
        public int? GameStudio { get; set; }
        [Column("digital")]
        public bool? Digital { get; set; }

        [InverseProperty("Game")]
        public virtual ICollection<GameImage> GameImages { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<Review> Reviews { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<WishList> WishLists { get; set; }

        [ForeignKey("Gameid")]
        [InverseProperty("Games")]
        public virtual ICollection<Category> Categories { get; set; }
        [ForeignKey("Gameid")]
        [InverseProperty("Games")]
        public virtual ICollection<GameFeature> GameFeatures { get; set; }
        [ForeignKey("Gameid")]
        [InverseProperty("Games")]
        public virtual ICollection<Platform> Platforms { get; set; }
    }
}
