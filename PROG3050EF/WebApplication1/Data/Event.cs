using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;

namespace GameStore.Data
{
    public partial class Event
    {
        public Event()
        {
            Customers = new HashSet<Customer>();
        }

        public int EventId { get; set; }
        [Required]
        public string Name { get; set; } = null!;

        [NotInPast()]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }

        public virtual ICollection<CustomerEvent> CustomerEvents { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
