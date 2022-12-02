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
    public class CartController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Game> data { get; set; }

        private IRepository<Merchandise> dataRep { get; set; }
        private ICart cart { get; set; }

        public CartController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Game> rep, IRepository<Merchandise> repMerch, ICart c)
        {
            data = rep;
            dataRep = repMerch;
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            cart = c;
            cart.Load(data, dataRep);
        }

        public ViewResult Index()
        {
            var vm = new CartViewModel
            {
                List = cart.List,
                SubTotal = cart.SubTotal
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult AddGame(int id)
        {
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
            return RedirectToAction("List", "Game");
        }

        [HttpPost]
        public RedirectToActionResult AddMerchandise(int id)
        {
            var merch = dataRep.Get(new QueryOptions<Merchandise>
            {
                Where = m => m.MerchId == id
            });
            if (merch == null)
            {
                TempData["message"] = "Unable to add merch to cart";
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
            return RedirectToAction("List", "Merchandise");
        }



        public IActionResult EditGame(int id)
        {
            CartItem item = cart.GetById(id, 0);

            
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View("Edit",item);
            }
        }

        public IActionResult EditMerchandise(int id)
        {
            CartItem item = cart.GetById(id, 1);

            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View("Edit",item);
            }
        }

        [HttpPost]
        public IActionResult EditMerchandise(CartItem item)
        {
            if (item.Merchandise.Quantity < item.Quantity)
            {
                item = cart.GetById(item.Merchandise.MerchId, 1);
                ModelState.AddModelError("", "Sorry, we do not have so much copies of this item in our store.");
                return View("Edit", item);
            }

            cart.EditMerchandise(item);
            cart.Save();

            TempData["message"] = $"{item.Merchandise.Name} updated";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditGame(CartItem item)
        {
            if (item.Game.Quantity < item.Quantity)
            {
                item = cart.GetById(item.Game.GameId, 0);
                ModelState.AddModelError("", "Sorry, we do not have so much copies of this item in our store.");
                return View("Edit", item);
            }


            cart.EditGame(item);
            cart.Save();

            TempData["message"] = $"{item.Game.Name} updated";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult RemoveGame(int id)
        {
            CartItem item = cart.GetById(id, 0);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Game.Name} removed from cart.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public RedirectToActionResult RemoveMerchandise(int id)
        {
            CartItem item = cart.GetById(id, 1);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Merchandise.Name} removed from cart.";
            return RedirectToAction("Index");
        }



        [HttpPost]
        public RedirectToActionResult Clear()
        {
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}
