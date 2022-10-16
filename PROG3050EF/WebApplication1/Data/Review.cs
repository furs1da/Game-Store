using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("Review")]
    public partial class Review
    {
        public Review()
        {
            ReviewImages = new HashSet<ReviewImage>();
        }

        [Key]
        [Column("review_id")]
        public int ReviewId { get; set; }
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("cust_id")]
        public int CustId { get; set; }
        [Column("rate")]
        public int Rate { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("title")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Title { get; set; }

        [ForeignKey("CustId")]
        [InverseProperty("Reviews")]
        public virtual Customer Cust { get; set; } = null!;
        [ForeignKey("GameId")]
        [InverseProperty("Reviews")]
        public virtual Game Game { get; set; } = null!;
        [InverseProperty("Review")]
        public virtual ICollection<ReviewImage> ReviewImages { get; set; }
    }
}
