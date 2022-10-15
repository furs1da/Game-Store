using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GameController : Controller
    {
        public GameController(TransactionContext transactionContext)
        {
            _transactionContext = transactionContext;
        }
        public IActionResult Index()
        {

            var transactions = _transactionContext.Game.ToList();
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
            return View("Edit", new Game());
        }
        [HttpPost()]
        public IActionResult Add(Game game)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to add the new transaction to the DB:
                _transactionContext.Game.Add(game);
                _transactionContext.SaveChanges();
                TempData["Message"] = game.Name + " Added";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                TempData["Message"] = "Failed to Add";
                ViewBag.Action = "Add";
                return View("Edit", game);
            }
        }

        // Defining GET & POST actions for the "Edit" sub/request resource:
        [HttpGet()]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";

            ViewBag.Company = _transactionContext.Game.OrderBy(g => g.Name).ToList();

            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _transactionContext.Game.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Edit(Game game)
        {
            if (ModelState.IsValid)
            {
                // it's valid so we want to update the existing transaction in the DB:
                _transactionContext.Game.Update(game);
                _transactionContext.SaveChanges();
                TempData["Message"] = game.Name + " Edited";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // it's invalid so we simply return the transaction object
                // to the Edit view setting add action again:

                ViewBag.Company = _transactionContext.Game.OrderBy(g => g.Name).ToList();

                ViewBag.Action = "Edit";
                TempData["Message"] = "Failed to Edit";
                return View(game);
            }
        }
        // Defining GET & POST actions for the "Delete" sub/request resource:
        [HttpGet()]
        public IActionResult Delete(int id)
        {
            // Find/retrieve/select the transaction to edit and then pass it to the view:
            var transaction = _transactionContext.Game.Find(id);
            return View(transaction);
        }

        [HttpPost()]
        public IActionResult Delete(Game game)
        {
            // Simply remove the transaction and then redirect back to the all transactions view:
            _transactionContext.Game.Remove(game);
            _transactionContext.SaveChanges();
            TempData["Message"] = game.Name + " Deleted";
            return RedirectToAction("Index", "Home");
        }
        private TransactionContext _transactionContext;
    }
}
