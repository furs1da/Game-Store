using GameStore.Models.DTOs;
using GameStore.Data;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Http;

namespace GameStore.Models.Grid
{
    public class GamesGridBuilder : GridBuilder
    {
        public GamesGridBuilder(ISession sess) : base(sess) { }

        public GamesGridBuilder(ISession sess, GamesGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Category.IndexOf(FilterPrefix.Category) == -1;
            routes.GameFeatureFilter = (isInitial) ? FilterPrefix.GameFeature + values.GameFeature : values.GameFeature;
            routes.CategoryFilter = (isInitial) ? FilterPrefix.Category + values.Category : values.Category;
            routes.PlatformFilter = (isInitial) ? FilterPrefix.Platform + values.Platform : values.Platform;
            routes.PriceFilter = (isInitial) ? FilterPrefix.Price + values.Price : values.Price;
            routes.NameFilter = (isInitial) ? FilterPrefix.Name + values.Name : values.Name;
        }

        public void LoadFilterSegments(string[] filter, Category category, Platform platform, GameFeature gameFeature)
        {
            if (category == null)
            {
                routes.CategoryFilter = FilterPrefix.Category + filter[0];
            }
            else
            {
                routes.CategoryFilter = FilterPrefix.Category + filter[0]
                    + "-" + category.Name.Slug();
            }

            if (platform == null)
            {
                routes.PlatformFilter = FilterPrefix.Platform + filter[1];
            }
            else
            {
                routes.PlatformFilter = FilterPrefix.Platform + filter[1]
                    + "-" + platform.Name.Slug();
            }

            if (gameFeature == null)
            {
                routes.GameFeatureFilter = FilterPrefix.GameFeature + filter[2];
            }
            else
            {
                routes.GameFeatureFilter = FilterPrefix.GameFeature + filter[2]
                    + "-" + gameFeature.Feature.Slug();
            }

            routes.PriceFilter = FilterPrefix.Price + filter[3];

            routes.NameFilter = FilterPrefix.Name + filter[4];
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string def = GamesGridDTO.DefaultFilter;
        string defName = GamesGridDTO.DefaultNameFilter;
        public bool IsFilterByCategory => routes.CategoryFilter != def;
        public bool IsFilterByPlatform => routes.PlatformFilter != def;
        public bool IsFilterByGameFeature => routes.GameFeatureFilter != def;
        public bool IsFilterByPrice => routes.PriceFilter != def;

        public bool IsFilterByName => routes.NameFilter != defName;

        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Game.Price));
    }
}
