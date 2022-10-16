using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data
{
    [Table("Platform")]
    public partial class Platform
    {
        public Platform()
        {
            Customers = new HashSet<Customer>();
            Games = new HashSet<Game>();
        }

        [Key]
        [Column("platform_id")]
        public int PlatformId { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;

        [InverseProperty("PreferedPlatform")]
        public virtual ICollection<Customer> Customers { get; set; }

        [ForeignKey("Platformid")]
        [InverseProperty("Platforms")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
