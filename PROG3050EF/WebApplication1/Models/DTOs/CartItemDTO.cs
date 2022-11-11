namespace GameStore.Models.DTOs
{
    public class CartItemDTO
    {
        public int GameId { get; set; }
        public int MerchandiseId { get; set; }
        public int Quantity { get; set; }
    }
}
