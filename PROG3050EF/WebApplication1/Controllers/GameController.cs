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
        //public GameController(IRepository<Game> rep) => data = rep;

        public RedirectToActionResult Index() => RedirectToAction("List");



        public GameController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Game> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
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





        #region Old Code
        /*
        public IActionResult Index()
        {
            var transactions = _storeContext.Games.ToList();
            // and then pass that off to the view to display:
            return View(transactions);
        }

        // ASP.NET Core MVC does the work of taking the new Transaction form
        // data and bundling/serializing it into a Transaction object that is
        // passed here as an input param

        // Defining GET & POST actions for the "Add" sub/request resource:
        [HttpGet()]
        public IActionResult Add()
        {
            // Because editing & adding new transactions will share the same view
            // we will set the action here:
            ViewBag.Action = "Add";

            // will look for a view named Add, not Edit
            return View("Edit", new Game());
        }
        [HttpPost()]
        public IActionResult Add(Game game)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to add the new transaction to the DB:
                _storeContext.Games.Add(game);
                _storeContext.SaveChanges();
                TempData["Message"] = game.Name + " Added";
                return RedirectToAction("Index", "Game");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                TempData["Message"] = "Failed to Add";
                ViewBag.Action = "Add";
                return View("Edit", game);
            }
        }

        // Defining GET & POST actions for the "Edit" sub/request resource:
        [HttpGet()]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Company = _storeContext.Games.OrderBy(g => g.Name).ToList();

            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _storeContext.Games.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to update the existing transaction in the DB:
                _storeContext.Games.Update(game);
                _storeContext.SaveChanges();
                TempData["Message"] = game.Name + " Edited";
                return RedirectToAction("Index", "Game");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                ViewBag.Company = _storeContext.Games.OrderBy(g => g.Name).ToList();

                ViewBag.Action = "Edit";
                TempData["Message"] = "Failed to Edit";
                return View(game);
            }
        }
        // Defining GET & POST actions for the "Delete" sub/request resource:
        [HttpGet()]
        public IActionResult Delete(int id)
        {
            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _storeContext.Games.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Delete(Game game)
        {
            // Simply remove the transaction and then redirect back to the all transactions view:
            _storeContext.Games.Remove(game);
            _storeContext.SaveChanges();
            TempData["Message"] = game.Name + " Deleted";
            return RedirectToAction("Index", "Game");
        }
        */
        #endregion
    }
}
