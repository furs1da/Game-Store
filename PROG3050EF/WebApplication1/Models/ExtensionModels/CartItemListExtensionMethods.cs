using System.Linq;
using System.Collections.Generic;
using GameStore.Models.DTOs;
using GameStore.Data.UtilityClasses;

namespace GameStore.Models.ExtensionModels
{
    public static class CartItemListExtensionMethods
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                GameId = ci.Game.GameId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
