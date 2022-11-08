using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ViewModels
{
    public class AddressViewModel
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(255)]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required.")]
        [MaxLength(55)]
        public string City { get; set; }
        [Required(ErrorMessage = "Province is required.")]
        public int Province { get; set; }
        [Required(ErrorMessage = "Postal Code is required.")]
        [MaxLength(16)]
        [ValidPostalCode()]
        public string PostalCode { get; set; }


        public List<Province> Provinces
        {
            get
            {
                return ProvinceList.Provinces;
            }
        }
        public bool IsSame { get; set; }

    }
}
