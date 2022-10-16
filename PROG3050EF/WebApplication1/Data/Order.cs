using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("Order")]
    public partial class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Column("order_no")]
        [StringLength(255)]
        [Unicode(false)]
        public string? OrderNo { get; set; }
        [Column("cust_id")]
        public int CustId { get; set; }
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("merchandise_id")]
        public int MerchandiseId { get; set; }
        [Column("date", TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column("isShipped")]
        public bool IsShipped { get; set; }

        [ForeignKey("CustId")]
        [InverseProperty("Orders")]
        public virtual Customer Cust { get; set; } = null!;
        [ForeignKey("GameId")]
        [InverseProperty("Orders")]
        public virtual Game Game { get; set; } = null!;
        [ForeignKey("MerchandiseId")]
        [InverseProperty("Orders")]
        public virtual Merchandise Merchandise { get; set; } = null!;
    }
}
