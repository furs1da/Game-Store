using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Models
{
    /// <summary>
    /// This class will inherit from the Entity Framework (EF) class
    /// call DbContext and is used by our code to interact with the DB.
    /// </summary>
    public class TransactionContext : DbContext
    {
        /// <summary>
        /// Define a constructor that simply passes the options argument
        /// to the base class constructor
        /// </summary>
        /// <param name="options"></param>
        public TransactionContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// This a public property that will allow our code full access to all the
        /// Category in the DB. The return type represents the
        /// result set we would get back from the server.
        /// </summary>
        public DbSet<Category> Category { get; set; }
        /// <summary>
        /// gives all Credit cards in database
        /// </summary>
        public DbSet<CreditCard> CreditCard { get; set; }
        /// <summary>
        /// gives all Customers in database
        /// </summary>
        public DbSet<Customer> Customer { get; set; }
        /// <summary>
        /// gives all Employees in database
        /// </summary>
        public DbSet<Employee> Employee { get; set; }
        /// <summary>
        /// gives all Events in database
        /// </summary>
        public DbSet<Event> Event { get; set; }
        /// <summary>
        /// gives all Friends in database
        /// </summary>   
        public DbSet<FriendsFamily> FriendsFamily { get; set; }
        /// <summary>
        /// gives all Games in database
        /// </summary>
        public DbSet<Game> Game { get; set; }
        /// <summary>
        /// gives all Game features in database
        /// </summary>        
        public DbSet<GameFeature> GameFeature { get; set; }
        /// <summary>
        /// gives all Game Images in database
        /// </summary>
        public DbSet<GameImage> GameImage { get; set; }
        /// <summary>
        /// gives all merchandise in database
        /// </summary>
        public DbSet<Merchandise> Merchandise { get; set; }
        /// <summary>
        /// gives all Merchandise images in database
        /// </summary>
        public DbSet<MerchandiseImage> MerchandiseImage { get; set; }
        /// <summary>
        /// gives all Orders in database
        /// </summary>
        public DbSet<Order> Order { get; set; }
        /// <summary>
        /// gives all Platforms in database
        /// </summary>
        public DbSet<Platform> Platform { get; set; }
        /// <summary>
        /// gives all Reviews in database
        /// </summary>
        public DbSet<Review> Review { get; set; }
        /// <summary>
        /// gives all Review Images in database
        /// </summary>
        public DbSet<ReviewImage> ReviewImage { get; set; }
        /// <summary>
        /// gives all Wishlists in database
        /// </summary>
        public DbSet<WishList> WishList { get; set; }

    }
}
