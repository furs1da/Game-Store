using GameStore.Models.DTOs;
using GameStore.Data;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Http;

namespace GameStore.Models.Grid
{
    public class MerchandiseGridBuilder : GridBuilder
    {
        public MerchandiseGridBuilder(ISession sess) : base(sess) { }

        public MerchandiseGridBuilder(ISession sess, MerchandiseGridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
            bool isInitial = values.Game.IndexOf(FilterPrefix.GameName) == -1;
            routes.GameNameFilter = (isInitial) ? FilterPrefix.GameName + values.Game : values.Game;
            routes.MerchandiseNameFilter = (isInitial) ? FilterPrefix.MerchName + values.Name : values.Name;
            routes.MerchandisePriceFilter = (isInitial) ? FilterPrefix.MerchPrice + values.Price : values.Price;
        }


        public void LoadFilterSegments(string[] filter)
        {
            routes.GameNameFilter = FilterPrefix.GameName + filter[0];

            routes.MerchandiseNameFilter = FilterPrefix.MerchName + filter[1];

            routes.MerchandisePriceFilter = FilterPrefix.MerchPrice + filter[2];
        }

        public void ClearFilterSegments() => routes.ClearFilters();

        string def = MerchandiseGridDTO.DefaultFilter;
        string defName = MerchandiseGridDTO.DefaultNameFilter;
        public bool IsFilterByGameName => routes.GameNameFilter != def;
        public bool IsFilterByMerchName => routes.MerchandiseNameFilter != defName;
        public bool IsFilterByPrice => routes.MerchandisePriceFilter != def;

        public bool IsSortByPrice =>
            routes.SortField.EqualsNoCase(nameof(Merchandise.Price));
    }
}
