using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string? OrderNo { get; set; }
        public int CustId { get; set; }
        public int Quantity { get; set; }
        public int? GameId { get; set; }
        public int? MerchandiseId { get; set; }
        public int CreditId { get; set; }
        public DateTime Date { get; set; }
        public bool IsShipped { get; set; }

        public virtual Customer Cust { get; set; } = null!;
        public virtual Game? Game { get; set; } = null!;
        public virtual Merchandise? Merchandise { get; set; } = null!;
        public virtual CreditCard CreditCard { get; set; } = null!;
    }
}
