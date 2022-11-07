using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class ShippingAddress
    {
        public ShippingAddress()
        {
            Customers = new HashSet<Customer>();
        }

        public int ShippingAddressId { get; set; }
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public int Province { get; set; }
        public string PostalCode { get; set; } = null!;

        public virtual ICollection<Customer> Customers { get; set; }
    }
}
