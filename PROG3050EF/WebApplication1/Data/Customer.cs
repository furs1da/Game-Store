using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("Customer")]
    [Index("Email", Name = "UQ__Customer__AB6E6164661522CF", IsUnique = true)]
    [Index("Nickname", Name = "UQ__Customer__CC6CD17E283CABFD", IsUnique = true)]
    public partial class Customer
    {
        public Customer()
        {
            CreditCards = new HashSet<CreditCard>();
            FriendsFamilyCustId1Navigations = new HashSet<FriendsFamily>();
            FriendsFamilyCustId2Navigations = new HashSet<FriendsFamily>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            WishLists = new HashSet<WishList>();
            Events = new HashSet<Event>();
        }

        [Key]
        [Column("cust_id")]
        public int CustId { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Nickname { get; set; } = null!;
        [Column("email")]
        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Column("password")]
        [StringLength(255)]
        [Unicode(false)]
        public string Password { get; set; } = null!;
        [Column("firstName")]
        [StringLength(255)]
        [Unicode(false)]
        public string? FirstName { get; set; }
        [Column("lastName")]
        [StringLength(255)]
        [Unicode(false)]
        public string? LastName { get; set; }
        [Column("gender")]
        public int? Gender { get; set; }
        [Column("dob", TypeName = "datetime")]
        public DateTime? Dob { get; set; }
        [Column("recievePromotion")]
        public bool? RecievePromotion { get; set; }
        [Column("shippingAddress")]
        [StringLength(255)]
        [Unicode(false)]
        public string? ShippingAddress { get; set; }
        [Column("shippingPostalCode")]
        [StringLength(16)]
        [Unicode(false)]
        public string? ShippingPostalCode { get; set; }
        [Column("mailingAddress")]
        [StringLength(255)]
        [Unicode(false)]
        public string? MailingAddress { get; set; }
        [Column("mailingPostalCode")]
        [StringLength(16)]
        [Unicode(false)]
        public string? MailingPostalCode { get; set; }
        [Column("preferedPlatform_id")]
        public int? PreferedPlatformId { get; set; }
        [Column("preferedCategory_id")]
        public int? PreferedCategoryId { get; set; }

        [ForeignKey("PreferedCategoryId")]
        [InverseProperty("Customers")]
        public virtual Category? PreferedCategory { get; set; } = null!;
        [ForeignKey("PreferedPlatformId")]
        [InverseProperty("Customers")]
        public virtual Platform? PreferedPlatform { get; set; } = null!;
        [InverseProperty("Cust")]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty("CustId1Navigation")]
        public virtual ICollection<FriendsFamily> FriendsFamilyCustId1Navigations { get; set; }
        [InverseProperty("CustId2Navigation")]
        public virtual ICollection<FriendsFamily> FriendsFamilyCustId2Navigations { get; set; }
        [InverseProperty("Cust")]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty("Cust")]
        public virtual ICollection<Review> Reviews { get; set; }
        [InverseProperty("Cust")]
        public virtual ICollection<WishList> WishLists { get; set; }

        [ForeignKey("Customerid")]
        [InverseProperty("Customers")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
