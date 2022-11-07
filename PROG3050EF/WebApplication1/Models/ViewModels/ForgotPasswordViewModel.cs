using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.ViewModels
{
    public class ForgotPasswordViewModel // Account Controller
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
