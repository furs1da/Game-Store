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
    public class OrderController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        private GameStoreUnitOfWork dataGameStore { get; set; }
        private IRepository<Order> data { get; set; }
        public OrderController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Order> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            dataGameStore = new GameStoreUnitOfWork(storeContext);
            data = rep;
        }

        [Authorize]
        public ViewResult List(GridDTO values)
        {
            var builder = new OrdersGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Order.OrderNo));

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            var options = new OrderQueryOptions
            {
                Includes = "Cust, Game, Merchandise",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize,
                Where = g => g.CustId == customer.CustId
            };
            options.SortFilter(builder);


            var vm = new GridViewModel<Order>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };

            return View(vm);
        }

        [Authorize]
        public ViewResult Details(string id)
        {
            var options = new OrderQueryOptions
            {
                Includes = "Cust, Game, Merchandise",
                Where = g => g.OrderNo == id
            };

            var eventList = data.List(options);

            return View(eventList);
        }


    }
}
