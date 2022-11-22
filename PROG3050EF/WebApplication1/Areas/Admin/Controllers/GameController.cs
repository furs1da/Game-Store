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
    public class GameController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;


        private GameStoreUnitOfWork dataGameStore { get; set; }
        private IRepository<Game> data { get; set; }
        //public GameController(IRepository<Game> rep) => data = rep;

        public RedirectToActionResult Index() => RedirectToAction("List");



        public GameController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Game> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            dataGameStore = new GameStoreUnitOfWork(storeContext);
            data = rep;
        }

        [Authorize]
        public ViewResult List(GamesGridDTO values)
        {
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
            var game = data.Get(new QueryOptions<Game>
            {
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform",
                Where = g => g.GameId == id
            });
            return View(game);
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


        [HttpGet]
        public ViewResult Add(int id) => GetGame(id, "Add");

        [HttpPost]
        public IActionResult Add(GameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                dataGameStore.LoadNewGameCategories(vm.Game, vm.SelectedCategories);
                dataGameStore.LoadNewPlatformGames(vm.Game, vm.SelectedPlatforms);
                dataGameStore.LoadNewGameFeatureGames(vm.Game, vm.SelectedGameFeatures);

                dataGameStore.Games.Insert(vm.Game);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Game.Name} added to Games.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                return View("Game", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetGame(id, "Edit");

        [HttpPost]
        public IActionResult Edit(GameViewModel vm)
        {
            if (ModelState.IsValid)
            {
                dataGameStore.DeleteCurrentGameCategories(vm.Game);
                dataGameStore.LoadNewGameCategories(vm.Game, vm.SelectedCategories);

                dataGameStore.DeleteCurrentPlatformGames(vm.Game);
                dataGameStore.LoadNewPlatformGames(vm.Game, vm.SelectedPlatforms);

                dataGameStore.DeleteCurrentGameFeatureGames(vm.Game);
                dataGameStore.LoadNewGameFeatureGames(vm.Game, vm.SelectedGameFeatures);

                dataGameStore.Games.Update(vm.Game);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Game.Name} updated.";
                return RedirectToAction("List");
            }
            else
            {
                Load(vm, "Edit");
                return View("Game", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetGame(id, "Delete");

        [HttpPost]
        public IActionResult Delete(GameViewModel vm)
        {
            dataGameStore.Games.Delete(vm.Game);
            dataGameStore.Save();
            TempData["message"] = $"{vm.Game.Name} removed from Games.";
            return RedirectToAction("List");
        }

        private ViewResult GetGame(int id, string operation)
        {
            var game = new GameViewModel();
            Load(game, operation, id);
            return View("Game", game);
        }
        private void Load(GameViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Game = new Game();
            }
            else
            {
                vm.Game = dataGameStore.Games.Get(new QueryOptions<Game>
                {
                    Includes = "GameCategories.Category, PlatformGames.Platform, GameFeatureGames.GameFeature",
                    Where = g => g.GameId == (id ?? vm.Game.GameId)
                });
            }

            vm.SelectedCategories = vm.Game.GameCategories?.Select(
                gc => gc.Category.CategoryId).ToArray();

            vm.Categories = dataGameStore.Categories.List(new QueryOptions<Category>
            {
                OrderBy = a => a.Name
            });


            vm.SelectedPlatforms = vm.Game.PlatformGames?.Select(
                pg => pg.Platform.PlatformId).ToArray();

            vm.Platforms = dataGameStore.Platforms.List(new QueryOptions<Platform>
            {
                OrderBy = a => a.Name
            });


            vm.SelectedGameFeatures = vm.Game.GameFeatureGames?.Select(
                gc => gc.GameFeature.FeatureId).ToArray();

            vm.GameFeatures = dataGameStore.GameFeatures.List(new QueryOptions<GameFeature>
            {
                OrderBy = a => a.Feature
            });



        }
    }
}
