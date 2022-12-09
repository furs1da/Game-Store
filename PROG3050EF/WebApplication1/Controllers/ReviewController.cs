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

namespace GameStore.Controllers
{
    public class ReviewController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        private GameStoreUnitOfWork dataGameStore { get; set; }
        private IRepository<Review> data { get; set; }

        public ReviewController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Review> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            dataGameStore = new GameStoreUnitOfWork(storeContext);
            _storeContext = storeContext;
            data = rep;
        }

        [Authorize]
        public ViewResult List(GridDTO values)
        {
            var builder = new ReviewGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Review.Title));

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            var options = new ReviewQueryOptions
            {
                Includes = "Cust, Game",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                Where = g => g.CustId == customer.CustId
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
    }
}
