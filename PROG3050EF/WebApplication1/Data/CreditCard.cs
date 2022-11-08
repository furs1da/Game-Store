using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class CreditCard
    {
        public int CreditId { get; set; }
        public int CustId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; } = null!;
        public DateTime ExpirationDate { get; set; }

        public virtual Customer Cust { get; set; } = null!;
    }
}
