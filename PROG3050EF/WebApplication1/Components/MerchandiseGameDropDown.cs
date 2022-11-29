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
    public class MerchandiseGameDropDown : ViewComponent
    {
        private IRepository<Game> data { get; set; }
        public MerchandiseGameDropDown(IRepository<Game> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var games = data.List(new QueryOptions<Game>
            {
                OrderBy = a => a.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = MerchandiseGridDTO.DefaultFilter,
                Items = games.ToDictionary(
                    a => a.GameId.ToString(), a => a.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
