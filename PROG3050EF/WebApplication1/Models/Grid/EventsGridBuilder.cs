using GameStore.Models.DTOs;
using GameStore.Data;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Http;

namespace GameStore.Models.Grid
{
    public class EventsGridBuilder : GridBuilder
    {
        public EventsGridBuilder(ISession sess) : base(sess) { }

        public EventsGridBuilder(ISession sess, GridDTO values,
            string defaultSortField) : base(sess, values, defaultSortField)
        {
           
        }
        public bool IsSortByDate =>
            routes.SortField.EqualsNoCase(nameof(Event.Date));
    }
}
