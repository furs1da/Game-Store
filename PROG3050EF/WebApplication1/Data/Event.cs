using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class Event
    {
        public Event()
        {
            Customers = new HashSet<Customer>();
        }

        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
        public string? Duration { get; set; }

        public virtual ICollection<CustomerEvent> CustomerEvents { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
