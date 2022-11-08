using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class Category
    {
        public Category()
        {
            Customers = new HashSet<Customer>();
            Games = new HashSet<Game>();
        }

        public int CategoryId { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<GameCategory> GameCategories { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
