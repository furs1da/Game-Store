using System.ComponentModel.DataAnnotations;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ViewModels
{
    public class ProfileViewModel // Profile Controller
    {
        public int? CustomerId { get; set; }

        [MaxLength(255)]
        public string? FirstName { get; set; }
        [MaxLength(255)]
        public string? LastName { get; set; }

        [Range(typeof(DateTime), "1/1/1900", "12/31/2015",
        ErrorMessage = "Date of Birth must be between 1/1/1900 and 12/31/2015")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date Format")]
        public DateTime? DOB { get; set; }

        public int? Gender { get; set; }

        public bool RecievePromotion { get; set; }

        public List<Gender> Genders { get
            {
                return GenderList.Genders;
            }
        }
    }
}
