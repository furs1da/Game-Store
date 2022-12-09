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
    public class AdminOrderController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        private GameStoreUnitOfWork dataGameStore { get; set; }
        private IRepository<Order> data { get; set; }
        public AdminOrderController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Order> rep)
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

            var options = new OrderQueryOptions
            {
                Includes = "Cust, Game, Merchandise",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
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

        private ViewResult GetOrders(string orderNo)
        {
            var orderList = new List<Order>();
            orderList = Load(orderNo);

            string currentUsername = orderList[0].Cust.Nickname;

            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ShippingAddress shippingAddress = _storeContext.ShippingAddresses.SingleOrDefault(addr => addr.ShippingAddressId == customer.ShippingAddressId);
            AddressViewModel vm = new AddressViewModel();


            if (shippingAddress != null)
            {
                vm.CustomerId = customer.CustId;
                vm.Address = shippingAddress.Address != null ? shippingAddress.Address : null;
                vm.City = shippingAddress.City != null ? shippingAddress.City : null;
                vm.Province = shippingAddress.Province != null ? shippingAddress.Province : 0;
                vm.PostalCode = shippingAddress.PostalCode != null ? shippingAddress.PostalCode : null;

                ViewBag.ShippingAddress = vm;
            }
                


            return View("Order", orderList);
        }
        private List<Order> Load(string? id = null)
        {
            var options = new OrderQueryOptions
            {
                Includes = "Cust, Game, Merchandise",
                Where = g => g.OrderNo == id
            };

            return data.List(options).ToList();
        }

        [HttpGet]
        public ViewResult Edit(string id) => GetOrders(id);

        [HttpPost]
        public IActionResult Edit(List<Order> vmList)
        {
            if (ModelState.IsValid)
            {
                foreach (Order item in vmList)
                {
                    dataGameStore.Orders.Update(item);
                }
                dataGameStore.Save();

                return RedirectToAction("List");
            }
            else
            {
                vmList = Load(vmList[0].OrderNo);

                string currentUsername = vmList[0].Cust.Nickname;

                Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
                ShippingAddress shippingAddress = _storeContext.ShippingAddresses.SingleOrDefault(addr => addr.ShippingAddressId == customer.ShippingAddressId);
                AddressViewModel vm = new AddressViewModel();


                if (shippingAddress != null)
                {
                    vm.CustomerId = customer.CustId;
                    vm.Address = shippingAddress.Address != null ? shippingAddress.Address : null;
                    vm.City = shippingAddress.City != null ? shippingAddress.City : null;
                    vm.Province = shippingAddress.Province != null ? shippingAddress.Province : 0;
                    vm.PostalCode = shippingAddress.PostalCode != null ? shippingAddress.PostalCode : null;

                    ViewBag.ShippingAddress = vm;
                }

                
                return View("Order", vmList);
            }
        }

        [HttpGet]
        public ViewResult Delete(string id) => GetOrders(id);

        [HttpPost]
        public IActionResult Delete(List<Order> vm)
        {
            foreach (Order item in vm)
            {
                dataGameStore.Orders.Delete(item);
            }

            dataGameStore.Save();
           
            return RedirectToAction("List");
        }
    }
}
