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
    public class AdminReviewController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        private GameStoreUnitOfWork dataGameStore { get; set; }
        private IRepository<Review> data { get; set; }

        public AdminReviewController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Review> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            dataGameStore = new GameStoreUnitOfWork(storeContext);
            data = rep;
        }


        [Authorize]
        public ViewResult List(GridDTO values)
        {
            var builder = new ReviewGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Event.Name));

            var options = new ReviewQueryOptions
            {
                Includes = "Cust, Game",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };
            options.SortFilter(builder);

            var vm = new GridViewModel<Review>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };
            vm.Items = vm.Items.Where(item => !string.IsNullOrEmpty(item.Title));
            return View(vm);
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var eventItem = data.Get(new QueryOptions<Review>
            {
                Includes = "Cust, Game",
                Where = g => g.ReviewId == id
            });
            return View(eventItem);
        }




        private ViewResult GetReview(int id, string operation)
        {
            var reviewItem = new ReviewApproveViewModel();
            Load(reviewItem, operation, id);
            return View("Review", reviewItem);
        }
        private void Load(ReviewApproveViewModel vm, string op, int? id = null)
        {
            if (Operation.IsAdd(op))
            {
                vm.Review = new Review();
            }
            else
            {
                vm.Review = dataGameStore.Reviews.Get(new QueryOptions<Review>
                {
                    Includes = "Cust, Game",
                    Where = e => e.ReviewId == (id ?? vm.Review.ReviewId)
                });
            }
        }


        [HttpGet]
        public ViewResult Edit(int id) => GetReview(id, "Edit");

        [HttpPost]
        public IActionResult Edit(ReviewApproveViewModel vm)
        {
            if (ModelState.IsValid)
            {
                dataGameStore.Reviews.Update(vm.Review);
                dataGameStore.Save();

                TempData["message"] = $"{vm.Review.Title} updated.";
                return RedirectToAction("List");
            }
            else
            {
                Load(vm, "Edit");
                return View("Event", vm);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id) => GetReview(id, "Delete");

        [HttpPost]
        public IActionResult Delete(ReviewApproveViewModel vm)
        {
            dataGameStore.Reviews.Delete(vm.Review);
            dataGameStore.Save();
            TempData["message"] = $"{vm.Review.Title} removed from Reviews.";
            return RedirectToAction("List");
        }

    }
}
