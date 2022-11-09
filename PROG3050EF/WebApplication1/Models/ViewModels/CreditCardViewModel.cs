using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ViewModels
{
    public class CreditCardViewModel
    {
        public int CreditId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }


        public List<NumericValue> Months
        {
            get
            {
                return NumericMonthList.Months;
            }
        }

        public List<NumericValue> Years
        {
            get
            {
                return NumericYearList.Years;
            }
        }
    }
}
