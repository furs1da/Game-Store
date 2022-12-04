using System.Linq;
using GameStore.Data;
using GameStore.Models.DTOs;
using GameStore.Models.Grid;
using GameStore.Models.ExtensionModels;

namespace GameStore.Models.Query
{
    public class ReviewQueryOptions : QueryOptions<Review>
    {
        public void SortFilter(ReviewGridBuilder builder)
        {
            if (builder.IsSortByDate)
            {
                OrderBy = g => g.Date;
            }
            else
            {
                OrderBy = g => g.Rate;
            }
        }
    }
}
