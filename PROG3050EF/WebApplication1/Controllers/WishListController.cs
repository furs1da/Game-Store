using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GameStore.Data;
using GameStore.Models.ViewModels;
using GameStore.Models.Recaptcha;
using GameStore.Helpers;
using Microsoft.Extensions.Options;
using GameStore.Interfaces;
using GameStore.Models.EmailSender;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class WishListController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        public WishListController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
        }

        [Authorize]
        public IActionResult MyList()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;
            ViewBag.UserName = User.Identity.Name;

            List<WishList> list = _storeContext.WishLists.Where(wl => wl.CustId == customer.CustId).Include(item => item.Game).Include(item => item.Merchandise).ToList();

            return View(list);
        }

        public IActionResult UserList(string username)
        {
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == username);
            if (customer == null)
            {
                ViewBag.UserName = username;
                return View(new List<WishList>());
            }

            ViewBag.UserId = customer.CustId;
            ViewBag.UserName = username;

            List<WishList> list = _storeContext.WishLists.Where(wl => wl.CustId == customer.CustId).Include(item => item.Game).Include(item => item.Merchandise).ToList();

            return View(list);
        }


        [Authorize]
        public IActionResult GameDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            Game game = _storeContext.Games.Where(item => item.GameId == id)
                .Include(item => item.GameCategories).ThenInclude(gameCategories => gameCategories.Category)
                .Include(item => item.PlatformGames).ThenInclude(platformGames => platformGames.Platform)
                .Include(item => item.GameFeatureGames).ThenInclude(gameFeatureGames => gameFeatureGames.GameFeature)
                .Include(item => item.WishLists)
                .FirstOrDefault();

            if (game == null)
                return View("Error");

            

            return View(game);
        }

        [Authorize]
        public IActionResult MerchandiseDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            Merchandise merch = _storeContext.Merchandises.Where(item => item.MerchId == id)
                .Include(item => item.WishLists)
                .FirstOrDefault();

            Game game = _storeContext.Games.FirstOrDefault(item => item.GameId == merch.GameId);

            ViewBag.Game = game;

            if (merch == null)
                return View("Error");

            return View(merch);
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddGameToWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.GameId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();
            //return RedirectToAction("MyList", new { id = id });

            return RedirectToAction("MyList");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddMerchandiseToWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.MerchandiseId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();

            return RedirectToAction("MyList");
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveGameFromWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = _storeContext.WishLists.Where(item => item.CustId == customer.CustId && item.GameId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }



            return RedirectToAction("MyList");
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveMerchandiseFromWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = _storeContext.WishLists.Where(item => item.CustId == customer.CustId && item.MerchandiseId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }



            return RedirectToAction("MyList");
        }












        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddGameToWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.GameId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();

            return RedirectToAction("GameDetails", new { id = id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveGameFromWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = _storeContext.WishLists.Where(item => item.CustId == customer.CustId && item.GameId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("GameDetails", new { id = id });
        }








        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddMerchandiseToWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = new WishList();
            wl.CustId = customer.CustId;
            wl.MerchandiseId = id;

            _storeContext.WishLists.Add(wl);
            _storeContext.SaveChanges();

            return RedirectToAction("MerchandiseDetails", new { id = id });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveMerchandiseFromWishListDetails(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            WishList wl = _storeContext.WishLists.Where(item => item.CustId == customer.CustId && item.MerchandiseId == id).FirstOrDefault();

            if (wl != null)
            {
                _storeContext.WishLists.Remove(wl);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("MerchandiseDetails", new { id = id });
        }

    }
}
