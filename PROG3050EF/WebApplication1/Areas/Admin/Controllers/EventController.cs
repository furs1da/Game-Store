using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;
using GameStore.Models.Grid;
using GameStore.Models.DTOs;
using GameStore.Models.Query;
using GameStore.Models.ViewModels;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameStore.Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EventController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;


        private GameStoreUnitOfWork dataGameStore { get; set; }
        private IRepository<Event> data { get; set; }
        public EventController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Event> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            dataGameStore = new GameStoreUnitOfWork(storeContext);
            data = rep;
        }

        [Authorize]
        public ViewResult List(GridDTO values)
        {
            var builder = new EventsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Event.Name));

            var options = new EventQueryOptions
            {
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Event>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var eventItem = data.Get(new QueryOptions<Event>
            {    
                Where = g => g.EventId == id
            });
            return View(eventItem);
        }

        [HttpGet]
        public ViewResult Add(int id) => GetEvent(id, "Add");

        [HttpPost]
        public IActionResult Add(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
               

                dataGameStore.Events.Insert(vm.Event);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Event.Name} added to Games.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Event", vm);
            }
        }

        private ViewResult GetEvent(int id, string operation)
        {
            var eventItem = new EventViewModel();
            Load(eventItem, operation, id);
            return View("Event", eventItem);
        }

        private void Load(EventViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Event = new Event();
            }
            else
            {
                vm.Event = dataGameStore.Events.Get(new QueryOptions<Event>
                {   
                    Where = e => e.EventId == (id ?? vm.Event.EventId)
                });
            }
        }


        [HttpGet]
        public ViewResult Edit(int id) => GetEvent(id, "Edit");

        [HttpPost]
        public IActionResult Edit(EventViewModel vm)
        {
            if (ModelState.IsValid)
            {
                dataGameStore.Events.Update(vm.Event);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Event.Name} updated.";
                return RedirectToAction("List");
            }
            else
            {
                Load(vm, "Edit");
                return View("Event", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetEvent(id, "Delete");

        [HttpPost]
        public IActionResult Delete(EventViewModel vm)
        {
            dataGameStore.DeleteCurrentCustomers(vm.Event);

            dataGameStore.Events.Delete(vm.Event);
            dataGameStore.Save();
            TempData["message"] = $"{vm.Event.Name} removed from Events.";
            return RedirectToAction("List");
        }

    }
}
