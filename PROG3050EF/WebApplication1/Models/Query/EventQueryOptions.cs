using System.Linq;
using GameStore.Data;
using GameStore.Models.DTOs;
using GameStore.Models.Grid;
using GameStore.Models.ExtensionModels;

namespace GameStore.Models.Query
{
    public class EventQueryOptions : QueryOptions<Event>
    {
        public void SortFilter(EventsGridBuilder builder)
        {     
            if (builder.IsSortByDate)
            {
                OrderBy = g => g.Date;
            }
            else
            {
                OrderBy = g => g.Name;
            }
        }
    }
}
