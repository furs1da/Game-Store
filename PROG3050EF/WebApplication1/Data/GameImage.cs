using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("GameImage")]
    public partial class GameImage
    {
        [Key]
        [Column("gameImg_id")]
        public int GameImgId { get; set; }
        [Column("game_id")]
        public int GameId { get; set; }
        [Column("path")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Path { get; set; }
        [Column("extention")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Extention { get; set; }
        [Column("isCoverImage")]
        public bool IsCoverImage { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("GameImages")]
        public virtual Game Game { get; set; } = null!;
    }
}
