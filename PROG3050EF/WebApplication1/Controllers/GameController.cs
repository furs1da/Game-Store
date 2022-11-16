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
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform, WishLists",
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

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            return View(vm);
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var game = data.Get(new QueryOptions<Game>
            {
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform, WishLists",
                Where = g => g.GameId == id
            });

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

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








        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToWistList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.GameId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();


            return RedirectToAction("List");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.GameId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = _storeContext.WishLists.Where(item => item.CustId == customer.CustId && item.GameId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = _storeContext.WishLists.Where(item => item.CustId == customer.CustId && item.GameId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }
    }
}
