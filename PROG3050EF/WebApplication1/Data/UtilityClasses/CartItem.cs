using Newtonsoft.Json;
using GameStore.Models.DTOs;

namespace GameStore.Data.UtilityClasses
{
    public class CartItem
    {
        public GameDTO Game { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public double SubTotal => (double)(Game.Price * Quantity);
    }
}
