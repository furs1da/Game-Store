using System;
using System.Collections.Generic;

namespace GameStore.Data
{
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

        public int CustId { get; set; }
        public string Nickname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool? RecievePromotion { get; set; }
        public int? PreferedPlatformId { get; set; }
        public int? PreferedCategoryId { get; set; }
        public int? MailingAddressId { get; set; }
        public int? ShippingAddressId { get; set; }

        public virtual MailingAddress? MailingAddress { get; set; }
        public virtual Category? PreferedCategory { get; set; }
        public virtual Platform? PreferedPlatform { get; set; }
        public virtual ShippingAddress? ShippingAddress { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public virtual ICollection<FriendsFamily> FriendsFamilyCustId1Navigations { get; set; }
        public virtual ICollection<FriendsFamily> FriendsFamilyCustId2Navigations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<WishList> WishLists { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
