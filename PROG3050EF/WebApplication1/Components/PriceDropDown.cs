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
    public class PriceDropDown : ViewComponent
    {
        public IViewComponentResult Invoke(string selectedValue)
        {
            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = GamesGridDTO.DefaultFilter,
                Items = new Dictionary<string, string> {
                    { "under10", "Under $10" },
                    { "10to35", "$10 to $35" },
                    { "over35", "Over $35" }
                }
            };

            return View(SharedPath.Select, vm);
        }
    }
}
