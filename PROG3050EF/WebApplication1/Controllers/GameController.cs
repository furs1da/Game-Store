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
using System.Text;


namespace GameStore.Controllers
{
    public class GameController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Game> data { get; set; }
        private IRepository<Merchandise> dataMerch { get; set; }
        private ICart cart { get; set; }

        public RedirectToActionResult Index() => RedirectToAction("List");

        public GameController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Game> rep, ICart cart, IRepository<Merchandise> repMerch)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            data = rep;
            dataMerch = repMerch;
            this.cart = cart;
            cart.Load(data, dataMerch);
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

            bool canBeDownloaded = false;

            if (game.Price == 0 || _storeContext.Orders.FirstOrDefault(item => item.GameId != null && item.GameId == game.GameId && item.CustId == customer.CustId) != null)
                canBeDownloaded = true;


            ViewBag.canBeDownloaded = canBeDownloaded;


            GameDetailsViewModel vm = new GameDetailsViewModel();
            
            vm.Game = game;

            Review rw = _storeContext.Reviews.FirstOrDefault(item => item.GameId == id && item.CustId == customer.CustId);

            if (rw == null)
            {
                vm.Rating = 0;
            }
            else
            {
                vm.Rating = rw.Rate;
            }

            List<Review> listRw = _storeContext.Reviews.Where(item => item.GameId == id).ToList();

            if (listRw.Count == 0)
            {
                vm.OverallRating = 0;
            }
            else
            {
                vm.OverallRating = listRw.Sum(item => item.Rate) / listRw.Count;
            }

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public RedirectToActionResult Filter([FromServices] IRepository<Category> dataCategory, [FromServices] IRepository<Platform> dataPlatform, 
            [FromServices] IRepository<GameFeature> dataFeature,
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
        [HttpPost]
        public async Task<IActionResult> AddToWistList(int id,
            [FromServices] IRepository<Category> dataCategory, [FromServices] IRepository<Platform> dataPlatform,
            [FromServices] IRepository<GameFeature> dataFeature,
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




            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.GameId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();


            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }

        [Authorize]
        [HttpPost]
        public RedirectToActionResult AddGame(int id, [FromServices] IRepository<Category> dataCategory, [FromServices] IRepository<Platform> dataPlatform,
            [FromServices] IRepository<GameFeature> dataFeature,
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




            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            var game = data.Get(new QueryOptions<Game>
            {
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform",
                Where = g => g.GameId == id
            });
            if (game == null)
            {
                TempData["message"] = "Unable to add book to cart";
            }
            else
            {
                var dto = new GameDTO();
                dto.Load(game);
                CartItem item = new CartItem
                {
                    Game = dto,
                    Quantity = 1  // default value
                };
                cart.AddGame(item);
                cart.Save();

                TempData["message"] = $"{game.Name} added to cart";
            }


            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }


        [Authorize]
        [HttpGet]
        public RedirectToActionResult AddGameDetails(int id, [FromServices] IRepository<Category> dataCategory, [FromServices] IRepository<Platform> dataPlatform,
            [FromServices] IRepository<GameFeature> dataFeature,
            string[] filter, bool clear = false)
        {
            
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            var game = data.Get(new QueryOptions<Game>
            {
                Includes = "GameCategories.Category, GameFeatureGames.GameFeature, PlatformGames.Platform",
                Where = g => g.GameId == id
            });
            if (game == null)
            {
                TempData["message"] = "Unable to add book to cart";
            }
            else
            {
                var dto = new GameDTO();
                dto.Load(game);
                CartItem item = new CartItem
                {
                    Game = dto,
                    Quantity = 1  // default value
                };
                cart.AddGame(item);
                cart.Save();

                TempData["message"] = $"{game.Name} added to cart";
            }


            return RedirectToAction("Details", new { id = id });
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
        public async Task<IActionResult> Download()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            byte[] bytes = Encoding.UTF8.GetBytes("Thank you for using our game store! Your steam code for this game is: 12345ABCDEFG");
            return File(bytes, "text/plain", "gamecode.txt");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromWishList(int id,
            [FromServices] IRepository<Category> dataCategory, [FromServices] IRepository<Platform> dataPlatform,
            [FromServices] IRepository<GameFeature> dataFeature,
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




            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            List<WishList> listWL = _storeContext.WishLists.Where(item => item.GameId != null).ToList();

            WishList wl = listWL.Where(item => item.CustId == customer.CustId && item.GameId == id).FirstOrDefault();


            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            List<WishList> listWL = _storeContext.WishLists.Where(item => item.GameId != null).ToList();

            WishList wl = listWL.Where(item => item.CustId == customer.CustId && item.GameId == id).FirstOrDefault();


            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Rating(int id, int rating)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            Review rw = _storeContext.Reviews.FirstOrDefault(item => item.GameId == id && item.CustId == customer.CustId);

            if(rw != null)
            {
                rw.Rate = rating;

                _storeContext.Reviews.Update(rw);
                _storeContext.SaveChanges();
            }
            else
            {
                rw = new Review();
                rw.GameId = id;
                rw.CustId = customer.CustId;
                rw.Rate = rating;

                _storeContext.Reviews.Add(rw);
                _storeContext.SaveChanges();
            }


            return RedirectToAction("Details", new { id = id });
        }
    }
}
