using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class WishList
    {
        public int WishId { get; set; }
        public int CustId { get; set; }
        public int GameId { get; set; }
        public int MerchandiseId { get; set; }

        public virtual Customer Cust { get; set; } = null!;
        public virtual Game Game { get; set; } = null!;
        public virtual Merchandise Merchandise { get; set; } = null!;
    }
}
