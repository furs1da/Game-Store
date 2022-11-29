using System.Linq;
using GameStore.Data;
using GameStore.Models.DTOs;
using GameStore.Models.Grid;
using GameStore.Models.ExtensionModels;

namespace GameStore.Models.Query
{
    public class MerchandiseQueryOptions : QueryOptions<Merchandise>
    {
        public void SortFilter(MerchandiseGridBuilder builder)
        {
            if (builder.IsFilterByPrice)
            {
                if (builder.CurrentRoute.PriceFilter == "under10")
                    Where = b => b.Price < 10;
                else if (builder.CurrentRoute.PriceFilter == "10to35")
                    Where = b => b.Price >= 10 && b.Price <= 35;
                else
                    Where = b => b.Price > 35;
            }

            if (builder.IsFilterByMerchName)
            {
                Where = b => b.Name.Contains(builder.CurrentRoute.MerchandiseNameFilter);
            }

            if (builder.IsFilterByGameName)
            {
                int id = builder.CurrentRoute.GameNameFilter.ToInt();
                if (id > 0)
                    Where = b => b.GameId == id;
            }

            if (builder.IsSortByPrice)
            {
                OrderBy = g => g.Price;
            }
            else
            {
                OrderBy = g => g.Name;
            }
        }
    }
}
