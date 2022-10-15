using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("Merchandise")]
    public partial class Merchandise
    {
        public Merchandise()
        {
            MerchandiseImages = new HashSet<MerchandiseImage>();
            Orders = new HashSet<Order>();
            WishLists = new HashSet<WishList>();
        }

        [Key]
        [Column("merch_id")]
        public int MerchId { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Name { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("game_id")]
        public int? GameId { get; set; }
        [Column("price", TypeName = "decimal(19, 2)")]
        public decimal? Price { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }

        [InverseProperty("Merchandise")]
        public virtual ICollection<MerchandiseImage> MerchandiseImages { get; set; }
        [InverseProperty("Merchandise")]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty("Merchandise")]
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
