using GameStore.Data;
using GameStore.Models.Grid;

namespace GameStore.Models.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<CartItem> List { get; set; }
        public double SubTotal { get; set; }
        public RouteDictionary GridRoute { get; set; }
    }
}
