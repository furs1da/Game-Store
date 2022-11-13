using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using GameStore.Models;
using GameStore.Data;

using GameStore.Models.ViewModels;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext _storeContext;


        public HomeController(ILogger<HomeController> logger, StoreContext storeContext)
        {
            _storeContext = storeContext;

        }

        public IActionResult Index()
        {
            return RedirectToAction("List", "Game");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}