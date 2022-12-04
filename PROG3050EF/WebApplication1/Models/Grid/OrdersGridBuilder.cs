using GameStore.Models.DTOs;
using GameStore.Data;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Http;

namespace GameStore.Models.Grid
{
    public class OrdersGridBuilder : GridBuilder
    {
        public OrdersGridBuilder(ISession sess) : base(sess) { }

        public OrdersGridBuilder(ISession sess, GridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {

        }
        public bool IsSortByDate =>
            routes.SortField.EqualsNoCase(nameof(Order.Date));
    }
}
