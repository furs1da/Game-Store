using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GameStore.Models.Repositories;
using GameStore.Models.Query;
using GameStore.Data;
using GameStore.Models.ViewModels;
using GameStore.Models.Grid;
using GameStore.Models.DTOs;

namespace GameStore.Components
{
    public class GameFeatureDropDown : ViewComponent
    {
        private IRepository<GameFeature> data { get; set; }
        public GameFeatureDropDown(IRepository<GameFeature> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var authors = data.List(new QueryOptions<GameFeature>
            {
                OrderBy = a => a.Feature
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = GamesGridDTO.DefaultFilter,
                Items = authors.ToDictionary(
                    a => a.FeatureId.ToString(), a => a.Feature)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
