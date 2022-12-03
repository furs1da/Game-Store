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
    public class CartController : Controller
    {
        const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
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
        [Authorize]
        public ViewResult Index()
        {
            var vm = new CartViewModel
            {
                List = cart.List,
                SubTotal = cart.SubTotal
            };
            return View(vm);
        }
        [Authorize]
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
        [Authorize]
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


        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpPost]
        public RedirectToActionResult RemoveGame(int id)
        {
            CartItem item = cart.GetById(id, 0);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Game.Name} removed from cart.";
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        public RedirectToActionResult RemoveMerchandise(int id)
        {
            CartItem item = cart.GetById(id, 1);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Merchandise.Name} removed from cart.";
            return RedirectToAction("Index");
        }


        [Authorize]
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Checkout()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            OrderViewModel orderViewModel = new OrderViewModel();

            orderViewModel.Cart = new CartViewModel
            {
                List = cart.List,
                SubTotal = cart.SubTotal
            };

            orderViewModel.CreditCards = _storeContext.CreditCards.Where(item => item.CustId == customer.CustId).ToList();

            orderViewModel.Order = new Order();

            orderViewModel.Order.CustId = customer.CustId;
            return View(orderViewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(OrderViewModel order)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            OrderViewModel orderViewModel = new OrderViewModel();

            orderViewModel.Cart = new CartViewModel
            {
                List = cart.List,
                SubTotal = cart.SubTotal
            };
            StringBuilder orderNo = new StringBuilder();
            while (true)
            {
                orderNo = new StringBuilder();
                var random = new Random();
                orderNo.Append('#');
                for (int i = 0; i < 10; i++)
                {
                    orderNo.Append(chars[random.Next(chars.Length)]);
                }

                Order testOrderNo = _storeContext.Orders.FirstOrDefault(item => item.OrderNo == orderNo.ToString());
                if (testOrderNo == null)
                {   
                    break;
                }
            }

            foreach (CartItem item in orderViewModel.Cart.List)
            {
                Order orderItem = new Order();
                
                orderItem.CustId = customer.CustId;
                orderItem.CreditId = order.CreditCardId;
                orderItem.Quantity = item.Quantity;

                if (item.Game != null)
                {
                    orderItem.GameId = item.Game.GameId;
                    orderItem.MerchandiseId = null;
                    if (item.Game.IsDigital != null && (bool)item.Game.IsDigital)
                    {
                        orderItem.IsShipped = true;
                    }
                    else
                    {
                        orderItem.IsShipped = false;
                    }
                }
                else
                {
                    orderItem.MerchandiseId = item.Merchandise.MerchId;
                    orderItem.GameId = null;
                    orderItem.IsShipped = false;
                }
                orderItem.Date = DateTime.Now;
                orderItem.OrderNo = orderNo.ToString();

                _storeContext.Orders.Update(orderItem);
                _storeContext.SaveChanges();
            }

            cart.Clear();
            cart.Save();
            return RedirectToAction("Index");
        }
    }
}
