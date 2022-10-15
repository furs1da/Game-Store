using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("MerchandiseImage")]
    public partial class MerchandiseImage
    {
        [Key]
        [Column("merchImg_id")]
        public int MerchImgId { get; set; }
        [Column("merchandise_id")]
        public int MerchandiseId { get; set; }
        [Column("path")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Path { get; set; }
        [Column("extention")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Extention { get; set; }

        [ForeignKey("MerchandiseId")]
        [InverseProperty("MerchandiseImages")]
        public virtual Merchandise Merchandise { get; set; } = null!;
    }
}
