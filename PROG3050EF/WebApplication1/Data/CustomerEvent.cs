using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public class CustomerEvent
    {
        public int Customerid { get; set; }
        public int Eventid { get; set; }

        public Customer Customer { get; set; }
        public Event Event { get; set; }
    }
}
