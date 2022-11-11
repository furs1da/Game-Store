using System.Collections.Generic;
using GameStore.Data;
using GameStore.Models.Grid;

namespace GameStore.Models.ViewModels
{
    public class GridViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public RouteDictionary CurrentRoute { get; set; }
        public int TotalPages { get; set; }
    }
}
