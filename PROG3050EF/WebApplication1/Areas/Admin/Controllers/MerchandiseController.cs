using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Admin.Controllers
{
    public class MerchandiseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
