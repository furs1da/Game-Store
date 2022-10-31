using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Data
{
    public class User : IdentityUser 
    {
        [NotMapped]
        public string? RoleName { get; set; }
    }
}
