using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Data
{
    public partial class Merchandise
    {
        public Merchandise()
        {
            MerchandiseImages = new HashSet<MerchandiseImage>();
            Orders = new HashSet<Order>();
            WishLists = new HashSet<WishList>();
        }

        public int MerchId { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Merchandise description cannot exceed 255 characters.")]
        public string? Description { get; set; }
        [Required]
        public int? GameId { get; set; }
        [Required]
        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public decimal? Price { get; set; }
        [Required]
        [Range(0, 100000, ErrorMessage = "Quantity must be more than 0.")]
        public int? Quantity { get; set; }

        public virtual ICollection<MerchandiseImage> MerchandiseImages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
