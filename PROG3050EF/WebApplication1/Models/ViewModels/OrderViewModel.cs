using System.ComponentModel.DataAnnotations;
using GameStore.Models.ValidationAttributes;
using GameStore.Data.Static_Data;
using GameStore.Data.UtilityClasses;
using GameStore.Data;

namespace GameStore.Models.ViewModels
{
    public class OrderViewModel
    {
        public CartViewModel Cart { get; set; }
        public Order Order { get; set; }
        public List<CreditCard> CreditCards { get; set; }

        public int CreditCardId { get; set; }
    }
}
