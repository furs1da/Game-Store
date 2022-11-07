using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class Platform
    {
        public Platform()
        {
            Customers = new HashSet<Customer>();
            Games = new HashSet<Game>();
        }

        public int PlatformId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
