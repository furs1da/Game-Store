using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GameStore.Data;
using GameStore.Models.ViewModels;
using GameStore.Models.Recaptcha;
using GameStore.Helpers;
using Microsoft.Extensions.Options;
using GameStore.Interfaces;
using GameStore.Models.EmailSender;
using static System.Net.WebRequestMethods;

namespace GameStore.Controllers
{
    public class CreditCardController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        public CreditCardController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public IActionResult Index()
        {

            var transactions = _storeContext.CreditCards.ToList();
            // and then pass that off to the view to display:
            return View(transactions);
        }

        // ASP.NET Core MVC does the work of taking the new Transaction form
        // data and bundling/serializing it into a Transaction object that is
        // passed here as an input param

        // Defining GET & POST actions for the "Add" sub/request resource:
        [HttpGet]
        public IActionResult Add()
        {
            // Because editing & adding new transactions will share the same view
            // we will set the action here:
            ViewBag.Action = "Add";
            ViewBag.Customers = _storeContext.Customers.ToList();
            // will look for a view named Add, not Edit
            return View("Edit", new CreditCard());
        }

        [HttpPost]
        public IActionResult Add(CreditCard card)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to add the new transaction to the DB:
                _storeContext.CreditCards.Add(card);
                _storeContext.SaveChanges();
                TempData["Message"] = card.CardName + " Added";
                return RedirectToAction("Index", "CreditCard");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                TempData["Message"] = "Failed to Add";
                ViewBag.Action = "Add";
                ViewBag.Customers = _storeContext.Customers.ToList();
                return View("Edit", card);
            }
        }

        // Defining GET & POST actions for the "Edit" sub/request resource:
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Company = _storeContext.CreditCards.OrderBy(g => g.CardName).ToList();
            ViewBag.Customers = _storeContext.Customers.ToList();
            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _storeContext.CreditCards.Find(id);
            return View(transaction);
        }

        [HttpPost]
        public IActionResult Edit(CreditCard card)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to update the existing transaction in the DB:
                _storeContext.CreditCards.Update(card);
                _storeContext.SaveChanges();
                TempData["Message"] = card.CardName + " Edited";
                return RedirectToAction("Index", "CreditCard");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                ViewBag.Company = _storeContext.CreditCards.OrderBy(g => g.CardName).ToList();
                ViewBag.Customers = _storeContext.Customers.ToList();
                ViewBag.Action = "Edit";
                TempData["Message"] = "Failed to Edit";
                return View(card);
            }
        }
        // Defining GET & POST actions for the "Delete" sub/request resource:
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _storeContext.CreditCards.Find(id);
            return View(transaction);
        }

        [HttpPost]
        public IActionResult Delete(CreditCard card)
        {
            // Simply remove the transaction and then redirect back to the all transactions view:
            _storeContext.CreditCards.Remove(card);
            _storeContext.SaveChanges();
            TempData["Message"] = card.CardName + " Deleted";
            return RedirectToAction("Index", "CreditCard");
        }
    }
}
