using System.Linq;
using GameStore.Data;
using GameStore.Models.DTOs;
using GameStore.Models.Grid;
using GameStore.Models.ExtensionModels;

namespace GameStore.Models.Query
{
    public class GameQueryOptions : QueryOptions<Game>
    {
        public void SortFilter(GamesGridBuilder builder)
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

            if (builder.IsFilterByCategory)
            {
                int id = builder.CurrentRoute.CategoryFilter.ToInt();
                if (id > 0)
                    Where = b => b.GameCategories.Any(gc => gc.Category.CategoryId == id);
            }

            if (builder.IsFilterByPlatform)
            {
                int id = builder.CurrentRoute.PlatformFilter.ToInt();
                if (id > 0)
                    Where = b => b.PlatformGames.Any(pg => pg.Platform.PlatformId == id);
            }

            if (builder.IsFilterByGameFeature)
            {
                int id = builder.CurrentRoute.GameFeatureFilter.ToInt();
                if (id > 0)
                    Where = b => b.GameFeatureGames.Any(gfg => gfg.GameFeature.FeatureId == id);
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
