using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GameStore.Data;
using GameStore.Models;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GameStore.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public IActionResult Index()
        {

            var transactions = _storeContext.Customers.ToList();
            // and then pass that off to the view to display:
            return View(transactions);
        }

        // ASP.NET Core MVC does the work of taking the new Transaction form
        // data and bundling/serializing it into a Transaction object that is
        // passed here as an input param

        // Defining GET & POST actions for the "Add" sub/request resource:
        [HttpGet()]
        public IActionResult Add()
        {
            // Because editing & adding new transactions will share the same view
            // we will set the action here:
            ViewBag.Action = "Add";

            // will look for a view named Add, not Edit
            return View("Edit", new Customer());
        }
        [HttpPost()]
        public IActionResult Add(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to add the new transaction to the DB:
                _storeContext.Customers.Add(customer);
                _storeContext.SaveChanges();
                TempData["Message"] = customer.Nickname + " Added";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                TempData["Message"] = "Failed to Add";
                ViewBag.Action = "Add";
                return View("Edit", customer);
            }
        }

        // Defining GET & POST actions for the "Edit" sub/request resource:
        [HttpGet()]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Company = _storeContext.Customers.OrderBy(g => g.Nickname).ToList();

            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _storeContext.Customers.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to update the existing transaction in the DB:
                _storeContext.Customers.Update(customer);
                _storeContext.SaveChanges();
                TempData["Message"] = customer.Nickname + " Edited";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                ViewBag.Company = _storeContext.Customers.OrderBy(g => g.Nickname).ToList();

                ViewBag.Action = "Edit";
                TempData["Message"] = "Failed to Edit";
                return View(customer);
            }
        }
        // Defining GET & POST actions for the "Delete" sub/request resource:
        [HttpGet()]
        public IActionResult Delete(int id)
        {
            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _storeContext.Customers.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Delete(Customer customer)
        {
            // Simply remove the transaction and then redirect back to the all transactions view:
            _storeContext.Customers.Remove(customer);
            _storeContext.SaveChanges();
            TempData["Message"] = customer.Nickname + " Deleted";
            return RedirectToAction("Index", "Login");
        }
        private StoreContext _storeContext;
    }
}
