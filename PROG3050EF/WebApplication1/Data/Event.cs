using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("Event")]
    public partial class Event
    {
        public Event()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        [Column("event_id")]
        public int EventId { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("date", TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("duration")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Duration { get; set; }

        [ForeignKey("Eventid")]
        [InverseProperty("Events")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
