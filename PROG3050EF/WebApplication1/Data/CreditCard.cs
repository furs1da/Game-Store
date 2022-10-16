using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("CreditCard")]
    public partial class CreditCard
    {
        [Key]
        [Column("credit_id")]
        public int CreditId { get; set; }
        [Column("cust_id")]
        public int CustId { get; set; }
        [Column("cardNumber")]
        public int CardNumber { get; set; }
        [Column("cardName")]
        [StringLength(255)]
        [Unicode(false)]
        public string CardName { get; set; } = null!;
        [Column("expirationDate", TypeName = "datetime")]
        public DateTime ExpirationDate { get; set; }

        [ForeignKey("CustId")]
        [InverseProperty("CreditCards")]
        public virtual Customer Cust { get; set; } = null!;
    }
}
