using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public LoginController(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }
        public IActionResult Index()
        {

            var transactions = _customerContext.Customer.ToList();
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
                _customerContext.Customer.Add(customer);
                _customerContext.SaveChanges();
                TempData["Message"] = customer.Nickname + " Added";
                return RedirectToAction("Index", "Home");
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

            ViewBag.Company = _customerContext.Customer.OrderBy(g => g.Nickname).ToList();

            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _customerContext.Customer.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to update the existing transaction in the DB:
                _customerContext.Customer.Update(customer);
                _customerContext.SaveChanges();
                TempData["Message"] = customer.Nickname + " Edited";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                ViewBag.Company = _customerContext.Customer.OrderBy(g => g.Nickname).ToList();

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
            var transaction = _customerContext.Customer.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Delete(Customer customer)
        {
            // Simply remove the transaction and then redirect back to the all transactions view:
            _customerContext.Customer.Remove(customer);
            _customerContext.SaveChanges();
            TempData["Message"] = customer.Nickname + " Deleted";
            return RedirectToAction("Index", "Company");
        }
        private CustomerContext _customerContext;
    }
}
