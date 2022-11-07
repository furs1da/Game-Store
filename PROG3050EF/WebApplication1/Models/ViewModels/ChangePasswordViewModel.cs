using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;

namespace GameStore.Models.ViewModels
{
    public class ChangePasswordViewModel // Profile Controller
    {
        [Required(ErrorMessage = "Please enter your old password.")]
        [DataType(DataType.Password)]
        [StrongPassword()]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter a new password.")]
        [DataType(DataType.Password)]
        [StrongPassword()]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your new password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
