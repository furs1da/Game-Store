using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Customers = new HashSet<Customer>();
            Games = new HashSet<Game>();
        }

        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("name")]
        [StringLength(255)]
        [Unicode(false)]
        public string? Name { get; set; }

        [InverseProperty("PreferedCategory")]
        public virtual ICollection<Customer> Customers { get; set; }

        [ForeignKey("Categoryid")]
        [InverseProperty("Categories")]
        public virtual ICollection<Game> Games { get; set; }
    }
}
