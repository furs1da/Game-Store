using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ViewModels
{
    public class CreditCardViewModel
    {
        public int CustomerId { get; set; }

        public string CardNumber { get; set; }
    }
}
