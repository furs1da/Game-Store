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
    public class CategoryDropDown : ViewComponent
    {
        private IRepository<Category> data { get; set; }
        public CategoryDropDown(IRepository<Category> rep) => data = rep;

        public IViewComponentResult Invoke(string selectedValue)
        {
            var authors = data.List(new QueryOptions<Category>
            {
                OrderBy = a => a.Name
            });

            var vm = new DropDownViewModel
            {
                SelectedValue = selectedValue,
                DefaultValue = GamesGridDTO.DefaultFilter,
                Items = authors.ToDictionary(
                    a => a.CategoryId.ToString(), a => a.Name)
            };

            return View(SharedPath.Select, vm);
        }
    }
}
