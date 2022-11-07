using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class Employee
    {
        public int EmpId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
