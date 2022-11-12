using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;
using GameStore.Models.Grid;
using GameStore.Models.DTOs;
using GameStore.Models.Query;
using GameStore.Models.ViewModels;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    public class GameController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Game> data { get; set; }
        public RedirectToActionResult Index() => RedirectToAction("List");

        public GameController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Game> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            data = rep;
        }


        [Authorize]
        public IActionResult List(GamesGridDTO values)
        {
            if(User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Game", new { area = "Admin" });
            }


            var builder = new GamesGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Game.Name));

            var options = new GameQueryOptions
            {
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Game>
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
            var book = data.Get(new QueryOptions<Game>
            {
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform",
                Where = g => g.GameId == id
            });
            return View(book);
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
