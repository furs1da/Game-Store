using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Data;
using GameStore.Models;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;
using GameStore.Models.Grid;
using GameStore.Models.DTOs;
using GameStore.Models.Query;
using GameStore.Models.ViewModels;
using GameStore.Models.ExtensionModels;
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    public class EventController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Event> data { get; set; }
        public RedirectToActionResult Index() => RedirectToAction("List");
        public EventController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext, IRepository<Event> rep)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
            data = rep;
        }
        [Authorize]
        public IActionResult List(GridDTO values)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("List", "Event", new { area = "Admin" });
            }


            var builder = new EventsGridBuilder(HttpContext.Session, values,
                defaultSortField: nameof(Event.Name));

            var options = new EventQueryOptions
            {
                Includes = "CustomerEvents",
                OrderByDirection = builder.CurrentRoute.SortDirection,
                PageNumber = builder.CurrentRoute.PageNumber,
                PageSize = builder.CurrentRoute.PageSize
            };

            options.SortFilter(builder);

            var vm = new GridViewModel<Event>
            {
                Items = data.List(options),
                CurrentRoute = builder.CurrentRoute,
                TotalPages = builder.GetTotalPages(data.Count)
            };


            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId =  customer.CustId;

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            CustomerEvent ce = new CustomerEvent();
            ce.Customerid = customer.CustId;
            ce.Eventid = id;

            _storeContext.CustomerEvents.Add(ce);
            _storeContext.SaveChanges();


            return RedirectToAction("List");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromWishList(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            CustomerEvent ce = _storeContext.CustomerEvents.Where(item => item.Customerid == customer.CustId && item.Eventid == id).FirstOrDefault();

            if (ce != null)
            {
                _storeContext.CustomerEvents.Remove(ce);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("List");
        }



        [Authorize]
        public ViewResult Details(int id)
        {
            var book = data.Get(new QueryOptions<Event>
            {
                Includes = "CustomerEvents",
                Where = g => g.EventId == id
            });
            return View(book);
        }

        #region OldCode
        //public IActionResult Index()
        //{

        //    var transactions = _storeContext.Events.ToList();
        //    // and then pass that off to the view to display:
        //    return View(transactions);
        //}


        //[HttpGet]
        //public IActionResult Add()
        //{
        //    // Because editing & adding new transactions will share the same view
        //    // we will set the action here:
        //    ViewBag.Action = "Add";

        //    // will look for a view named Add, not Edit
        //    return View("Edit", new Event());
        //}
        //[HttpPost]
        //public IActionResult Add(Event eventInstance)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // it's valid so we want to add the new transaction to the DB:
        //        _storeContext.Events.Add(eventInstance);
        //        _storeContext.SaveChanges();
        //        TempData["Message"] = eventInstance.Name + " Added";
        //        return RedirectToAction("Index", "Event");
        //    }
        //    else
        //    {
        //        // it's invalid so we simply return the transaction object
        //        // to the Edit view setting add action again:

        //        TempData["Message"] = "Failed to Add";
        //        ViewBag.Action = "Add";
        //        return View("Edit", eventInstance);
        //    }
        //}

        //// Defining GET & POST actions for the "Edit" sub/request resource:
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    ViewBag.Action = "Edit";

        //    ViewBag.Company = _storeContext.Events.OrderBy(g => g.Name).ToList();

        //    // Find/retrieve/select the transaction to edit and then pass it to the view:
        //    var transaction = _storeContext.Events.Find(id);
        //    return View(transaction);
        //}

        //[HttpPost]
        //public IActionResult Edit(Event eventInstance)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // it's valid so we want to update the existing transaction in the DB:
        //        _storeContext.Events.Update(eventInstance);
        //        _storeContext.SaveChanges();
        //        TempData["Message"] = eventInstance.Name + " Edited";
        //        return RedirectToAction("Index", "Event");
        //    }
        //    else
        //    {
        //        // it's invalid so we simply return the transaction object
        //        // to the Edit view setting add action again:

        //        ViewBag.Company = _storeContext.Events.OrderBy(g => g.Name).ToList();

        //        ViewBag.Action = "Edit";
        //        TempData["Message"] = "Failed to Edit";
        //        return View(eventInstance);
        //    }
        //}
        //// Defining GET & POST actions for the "Delete" sub/request resource:
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    // Find/retrieve/select the transaction to edit and then pass it to the view:
        //    var transaction = _storeContext.Events.Find(id);
        //    return View(transaction);
        //}

        //[HttpPost]
        //public IActionResult Delete(Event eventInstance)
        //{
        //    // Simply remove the transaction and then redirect back to the all transactions view:
        //    _storeContext.Events.Remove(eventInstance);
        //    _storeContext.SaveChanges();
        //    TempData["Message"] = eventInstance.Name + " Deleted";
        //    return RedirectToAction("Index", "Event");
        //}
        //private StoreContext _storeContext;
        #endregion
    }
}
