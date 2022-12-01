using System.Linq;
using System.Collections.Generic;
using GameStore.Models.DTOs;
using GameStore.Data.UtilityClasses;
using GameStore.Data;

namespace GameStore.Models.ExtensionModels
{
    public static class CartItemListExtensionMethods
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                GameId = ci.Game != null ? ci.Game.GameId : null,
                MerchandiseId = ci.Merchandise != null ? ci.Merchandise.MerchId : null,
                Quantity = ci.Quantity
            }).ToList();
    }
}
