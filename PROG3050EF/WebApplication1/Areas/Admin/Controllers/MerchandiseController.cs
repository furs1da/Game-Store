using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;
using GameStore.Models.Grid;
using GameStore.Models.DTOs;
using GameStore.Models.Query;
using GameStore.Models.ViewModels;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GameStore.Data;
using GameStore.Data.UtilityClasses;

namespace GameStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MerchandiseController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        private IRepository<Merchandise> data { get; set; }
        private GameStoreUnitOfWork dataGameStore { get; set; }
        public RedirectToActionResult Index() => RedirectToAction("List");

        public MerchandiseController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Merchandise> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            dataGameStore = new GameStoreUnitOfWork(storeContext);
            data = rep;
        }

        [Authorize]
        public ViewResult List(MerchandiseGridDTO values)
        {
            var builder = new MerchandiseGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Merchandise.Name));

            var options = new MerchandiseQueryOptions
            {
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


            return View(vm);
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var game = data.Get(new QueryOptions<Merchandise>
            { 
                Where = g => g.MerchId == id
            });
            return View(game);
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

        [HttpGet]
        public ViewResult Add(int id) => GetMerchandise(id, "Add");

        [HttpPost]
        public IActionResult Add(MerchandiseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                dataGameStore.Merchandises.Insert(vm.Merchandise);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Merchandise.Name} added to Merchandise.";
                return RedirectToAction("Index");
            }
            else
            {
                Load(vm, "Add");
                ViewBag.Games = _storeContext.Games.ToList();
                return View("Merchandise", vm);
            }
        }

        [HttpGet]
        public ViewResult Edit(int id) => GetMerchandise(id, "Edit");
        [HttpPost]
        public IActionResult Edit(MerchandiseViewModel vm)
        {
            if (ModelState.IsValid)
            {
               

                dataGameStore.Merchandises.Update(vm.Merchandise);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Merchandise.Name} updated.";
                return RedirectToAction("List");
            }
            else
            {
                Load(vm, "Edit");
                ViewBag.Games = _storeContext.Games.ToList();
                return View("Merchandise", vm);
            }
        }


        [HttpGet]
        public ViewResult Delete(int id) => GetMerchandise(id, "Delete");

        [HttpPost]
        public IActionResult Delete(MerchandiseViewModel vm)
        {

            dataGameStore.Merchandises.Delete(vm.Merchandise);
            dataGameStore.Save();
            TempData["message"] = $"{vm.Merchandise.Name} removed from Merchandise.";
            return RedirectToAction("List");
        }






        private ViewResult GetMerchandise(int id, string operation)
        {
            var merchandise = new MerchandiseViewModel();
            Load(merchandise, operation, id);
            ViewBag.Games = _storeContext.Games.ToList();
            return View("Merchandise", merchandise);
        }

        private void Load(MerchandiseViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Merchandise = new Merchandise();
            }
            else
            {
                vm.Merchandise = dataGameStore.Merchandises.Get(new QueryOptions<Merchandise>
                {
                    Where = g => g.GameId == (id ?? vm.Merchandise.MerchId)
                });
            }
        }
    }
}
