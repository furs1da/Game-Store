using System;
using System.Collections.Generic;

namespace GameStore.Data
{
    public partial class FriendsFamily
    {
        public int FriendId { get; set; }
        public int CustId1 { get; set; }
        public int CustId2 { get; set; }
        public bool Status { get; set; }
        public DateTime DateConnected { get; set; }
        public bool IsFamily { get; set; }

        public virtual Customer CustId1Navigation { get; set; } = null!;
        public virtual Customer CustId2Navigation { get; set; } = null!;
    }
}
