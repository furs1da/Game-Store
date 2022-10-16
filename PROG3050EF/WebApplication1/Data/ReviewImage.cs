using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("ReviewImage")]
    public partial class ReviewImage
    {
        [Key]
        [Column("reviewImg_id")]
        public int ReviewImgId { get; set; }
        [Column("path")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Path { get; set; }
        [Column("extention")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Extention { get; set; }
        [Column("review_id")]
        public int ReviewId { get; set; }

        [ForeignKey("ReviewId")]
        [InverseProperty("ReviewImages")]
        public virtual Review Review { get; set; } = null!;
    }
}
