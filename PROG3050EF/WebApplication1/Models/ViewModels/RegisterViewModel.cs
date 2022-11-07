using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;

namespace GameStore.Models.ViewModels
{
    public class RegisterViewModel // Account Controller
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a nickname.")]
        [StringLength(255)]
        [NicknameUnique(nameof(Id))]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Please enter an email.")]
        [EmailAddress]
        [StringLength(255)]
        [UniqueEmail(nameof(Id))]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [StrongPassword()]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string SiteKey { get; set; }

    }
}
