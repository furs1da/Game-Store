using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Admin.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
