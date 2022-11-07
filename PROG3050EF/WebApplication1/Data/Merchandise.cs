using System;
using System.Collections.Generic;

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
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? GameId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<MerchandiseImage> MerchandiseImages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }
    }
}
