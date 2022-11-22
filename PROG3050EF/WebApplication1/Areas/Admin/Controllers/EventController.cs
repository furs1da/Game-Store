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


        [Authorize]
        [HttpPost]
        public RedirectToActionResult Filter([FromServices] IRepository<Category> dataCategory, [FromServices] IRepository<Platform> dataPlatform, [FromServices] IRepository<GameFeature> dataFeature,
            string[] filter, bool clear = false)
        {
            var builder = new GamesGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                var category = dataCategory.Get(filter[0].ToInt());
                var platform = dataPlatform.Get(filter[1].ToInt());
                var gameFeature = dataFeature.Get(filter[2].ToInt());

                builder.LoadFilterSegments(filter, category, platform, gameFeature);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }
    }
}
