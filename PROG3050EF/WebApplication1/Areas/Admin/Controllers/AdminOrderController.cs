using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Admin.Controllers
{
    public class AdminOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
