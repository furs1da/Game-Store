using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("WishList")]
    public partial class WishList
    {
        [Key]
        [Column("wish_id")]
        public int WishId { get; set; }
        [Column("cust_id")]
        public int CustId { get; set; }
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("merchandise_id")]
        public int MerchandiseId { get; set; }

        [ForeignKey("CustId")]
        [InverseProperty("WishLists")]
        public virtual Customer Cust { get; set; } = null!;
        [ForeignKey("GameId")]
        [InverseProperty("WishLists")]
        public virtual Game Game { get; set; } = null!;
        [ForeignKey("MerchandiseId")]
        [InverseProperty("WishLists")]
        public virtual Merchandise Merchandise { get; set; } = null!;
    }
}
