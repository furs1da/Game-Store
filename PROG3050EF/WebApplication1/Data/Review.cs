using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class Review
    {
        public Review()
        {
            ReviewImages = new HashSet<ReviewImage>();
        }

        public int ReviewId { get; set; }
        public int GameId { get; set; }
        public int CustId { get; set; }
        public int Rate { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }

        public bool? IsApproved { get; set; }

        public virtual Customer Cust { get; set; } = null!;
        public virtual Game Game { get; set; } = null!;
        public virtual ICollection<ReviewImage> ReviewImages { get; set; }
    }
}
