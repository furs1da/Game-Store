using GameStore.Data;

namespace GameStore.Models.DTOs
{
    public class MerchandiseDTO
    {
        public int MerchId { get; set; }
        public string? Name { get; set; }
        
        public string? Description { get; set; }
      
        public int? GameId { get; set; }

        public decimal? Price { get; set; }
       
        public int? Quantity { get; set; }

        public void Load(Merchandise merch)
        {
            MerchId = merch.MerchId;
            Name = merch.Name;
            Description = merch.Description;
            GameId = merch.GameId;

            Price = merch.Price;
            Quantity = merch.Quantity;

        }
    }
}
