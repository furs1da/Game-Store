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
    public class MerchandiseController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Merchandise> data { get; set; }
        private IRepository<Game> dataGame { get; set; }
        private ICart cart { get; set; }
        public RedirectToActionResult Index() => RedirectToAction("List");

        public MerchandiseController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Merchandise> rep, ICart cart, IRepository<Game> repGame)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            data = rep;
            dataGame = repGame;
            this.cart = cart;
            cart.Load(dataGame, data);
        }

        [Authorize]
        public IActionResult List(MerchandiseGridDTO values)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Merchandise", new { area = "Admin" });
            }


            var builder = new MerchandiseGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Merchandise.Name));

            var options = new MerchandiseQueryOptions
            {
                Includes = "WishLists",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Merchandise>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            ViewBag.Games = _storeContext.Games.ToList();

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            return View(vm);
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var merch = data.Get(new QueryOptions<Merchandise>
            {
                Includes = "WishLists",
                Where = m => m.MerchId == id
            });

            

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            ViewBag.Games = _storeContext.Games.ToList();
            return View(merch);
        }


        [Authorize]
        [HttpPost]
        public RedirectToActionResult Filter(string[] filter, bool clear = false)
        {
            var builder = new MerchandiseGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                builder.LoadFilterSegments(filter);
            }

            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWistList(int id, string[] filter, bool clear = false)
        {
            var builder = new MerchandiseGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                builder.LoadFilterSegments(filter);
            }

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.MerchandiseId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();


            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }

        [Authorize]
        [HttpPost]
        public RedirectToActionResult AddMerchandise(int id,string[] filter, bool clear = false)
        {
            var builder = new MerchandiseGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                builder.LoadFilterSegments(filter);
            }





            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            var merch = data.Get(new QueryOptions<Merchandise>
            {    
                Where = m => m.MerchId == id
            });
            if (merch == null)
            {
                TempData["message"] = "Unable to add book to cart";
            }
            else
            {
                var dto = new MerchandiseDTO();
                dto.Load(merch);
                CartItem item = new CartItem
                {
                    Merchandise = dto,
                    Quantity = 1  // default value
                };
                cart.AddMerchandise(item);
                cart.Save();

                TempData["message"] = $"{merch.Name} added to cart";
            }


            builder.SaveRouteSegments();
            return RedirectToAction("List", builder.CurrentRoute);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.MerchandiseId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromWishList(int id, string[] filter, bool clear = false)
        {
            var builder = new MerchandiseGridBuilder(HttpContext.Session);

            if (clear)
            {
                builder.ClearFilterSegments();
            }
            else
            {
                builder.LoadFilterSegments(filter);
            }




            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            List<WishList> listWL = _storeContext.WishLists.Where(item => item.MerchandiseId != null).ToList();

            WishList wl = listWL.Where(item => item.CustId == customer.CustId && item.MerchandiseId == id).FirstOrDefault();
  
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

            List<WishList> listWL = _storeContext.WishLists.Where(item => item.MerchandiseId != null).ToList();

            WishList wl = listWL.Where(item => item.CustId == customer.CustId && item.MerchandiseId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
        }
    }
}
