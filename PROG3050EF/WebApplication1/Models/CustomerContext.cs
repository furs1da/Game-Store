using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    public class CustomerContext : DbContext
    {
        /// <summary>
        /// Define a constructor that simply passes the options argument
        /// to the base class constructor
        /// </summary>
        /// <param name="options"></param>
        public CustomerContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// This a public property that will allow our code full access to all the
        /// Category in the Customer table in the DB. The return type represents the
        /// result set we would get back from the server.
        /// </summary>
        public DbSet<Customer> Customer { get; set; }

    }
}
