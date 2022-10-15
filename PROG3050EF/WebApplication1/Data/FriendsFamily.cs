using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("FriendsFamily")]
    public partial class FriendsFamily
    {
        [Key]
        [Column("friend_id")]
        public int FriendId { get; set; }
        [Column("cust_id1")]
        public int CustId1 { get; set; }
        [Column("cust_id2")]
        public int CustId2 { get; set; }
        [Column("status")]
        public bool Status { get; set; }
        [Column("dateConnected", TypeName = "datetime")]
        public DateTime DateConnected { get; set; }
        [Column("isFamily")]
        public bool IsFamily { get; set; }

        [ForeignKey("CustId1")]
        [InverseProperty("FriendsFamilyCustId1Navigations")]
        public virtual Customer CustId1Navigation { get; set; } = null!;
        [ForeignKey("CustId2")]
        [InverseProperty("FriendsFamilyCustId2Navigations")]
        public virtual Customer CustId2Navigation { get; set; } = null!;
    }
}
