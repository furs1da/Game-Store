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
    public class PlatformDropDown : ViewComponent
    {
        private IRepository<Platform> data { get; set; }
        public PlatformDropDown(IRepository<Platform> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var authors = data.List(new QueryOptions<Platform>
            {
                OrderBy = a => a.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = GamesGridDTO.DefaultFilter,
                Items = authors.ToDictionary(
                    a => a.PlatformId.ToString(), a => a.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
