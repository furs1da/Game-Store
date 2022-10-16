using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("GameFeature")]
    public partial class GameFeature
    {
        public GameFeature()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        [Column("feature_id")]
        public int FeatureId { get; set; }
        [Column("feature")]
        [StringLength(255)]
        [Unicode(false)]
        public string Feature { get; set; } = null!;

        [ForeignKey("GameFeatureid")]
        [InverseProperty("GameFeatures")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
