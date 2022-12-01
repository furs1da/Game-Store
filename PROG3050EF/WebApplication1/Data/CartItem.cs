using Newtonsoft.Json;
using GameStore.Models.DTOs;

namespace GameStore.Data
{
    public class CartItem
    {
        public GameDTO Game { get; set; }
        public MerchandiseDTO Merchandise { get; set; }
        public int Quantity { get; set; }
        public double SubTotal()
        {
            if(Game != null)
            {
                return (double)(Game.Price * Quantity);
            }
            else
            {
                return (double)(Merchandise.Price * Quantity);
            }

        }
    }
}
