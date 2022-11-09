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
using Microsoft.AspNetCore.Authorization;

namespace GameStore.Controllers
{
    public class CreditCardController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        public CreditCardController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            var creditCards = _storeContext.CreditCards.Where(card => card.CustId == customer.CustId).ToList();

            List<CreditCardViewModel> creditCardList = new List<CreditCardViewModel>();

            if(creditCards.Count() > 0)
            {
                foreach(CreditCard item in creditCards)
                {
                    creditCardList.Add(new CreditCardViewModel()
                    {
                        CustomerId = customer.CustId,
                        CreditId = item.CreditId,
                        CardName = item.CardName,
                        CardNumber = "**** **** **** " + item.CardNumber.Substring(12),
                        Month = item.ExpirationDate.Month,
                        Year = int.Parse(item.ExpirationDate.ToString("yy"))
                    });
                }

            }
            
            return View(creditCardList);
        }

        // ASP.NET Core MVC does the work of taking the new Transaction form
        // data and bundling/serializing it into a Transaction object that is
        // passed here as an input param

        // Defining GET & POST actions for the "Add" sub/request resource:
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            ViewBag.Action = "Add";

            CreditCardViewModel vm = new CreditCardViewModel();
            vm.CustomerId = customer.CustId;

            return View("Edit", vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(CreditCardViewModel card)
        {
            if(card.Month <= DateTime.Now.Month && card.Year <= int.Parse(DateTime.Now.ToString("yy")))
            {
                ModelState.AddModelError("", "Expiration Date must be in the future!");
            }

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (card.CustomerId != customer.CustId)
                return View("Error");

            CreditCard creditCardDuplication = _storeContext.CreditCards.FirstOrDefault(cc => cc.CardNumber == card.CardNumber && cc.CustId == customer.CustId);

            if(creditCardDuplication != null)
            {
                ModelState.AddModelError("", "This card is already added to the account.");
            }


            if (ModelState.IsValid)
            {
                CreditCard creditCard = new CreditCard
                {
                    CustId = card.CustomerId,
                    CardName = card.CardName,
                    CardNumber = card.CardNumber.Trim(),
                    ExpirationDate = new DateTime(card.Year + 2000, card.Month, 1)
                };

                _storeContext.CreditCards.Add(creditCard);
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

                return View("Edit", card);
            }
        }

        // Defining GET & POST actions for the "Edit" sub/request resource:
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            CreditCard creditCard = _storeContext.CreditCards.SingleOrDefault(card => card.CreditId == id);

            if (creditCard == null)
                return View("Error");

            if(creditCard.CustId != customer.CustId) 
                return View("Error");


            CreditCardViewModel vm = new CreditCardViewModel
            {
                CustomerId = customer.CustId,
                CreditId = creditCard.CreditId,
                CardName = creditCard.CardName,
                CardNumber = creditCard.CardNumber,
                Month = creditCard.ExpirationDate.Month,
                Year = int.Parse(creditCard.ExpirationDate.ToString("yy"))
            };

            ViewBag.Action = "Edit";

            return View(vm);
        }


        [Authorize]
        [HttpPost]
        public IActionResult Edit(CreditCardViewModel card)
        {
            if (card.Month <= DateTime.Now.Month && card.Year <= int.Parse(DateTime.Now.ToString("yy")))
            {
                ModelState.AddModelError("", "Expiration Date must be in the future!");
            }

            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            CreditCard creditCard = _storeContext.CreditCards.SingleOrDefault(cc => cc.CreditId == card.CreditId);

            if (creditCard == null)
                return View("Error");

            if (creditCard.CustId != customer.CustId)
                return View("Error");

            if (ModelState.IsValid)
            {
                creditCard.CardName = card.CardName;
                creditCard.CardNumber = card.CardNumber.Trim();
                creditCard.ExpirationDate = new DateTime(card.Year + 2000, card.Month, 1);
                _storeContext.CreditCards.Update(creditCard);
                _storeContext.SaveChanges();

                TempData["Message"] = card.CardName + " Edited";
                return RedirectToAction("Index", "CreditCard");
            }
            else
            {
                ViewBag.Action = "Edit";
                TempData["Message"] = "Failed to Edit";
                return View(card);
            }
        }


        // Defining GET & POST actions for the "Delete" sub/request resource:
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            CreditCard creditCard = _storeContext.CreditCards.SingleOrDefault(card => card.CreditId == id);

            if (creditCard == null)
                return View("Error");

            if (creditCard.CustId != customer.CustId)
                return View("Error");


            CreditCardViewModel vm = new CreditCardViewModel
            {
                CustomerId = customer.CustId,
                CreditId = creditCard.CreditId,
                CardName = creditCard.CardName,
                CardNumber = creditCard.CardNumber,
                Month = creditCard.ExpirationDate.Month,
                Year = int.Parse(creditCard.ExpirationDate.ToString("yy"))
            };


            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(CreditCardViewModel card)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            CreditCard creditCard = _storeContext.CreditCards.SingleOrDefault(cc => cc.CreditId == card.CreditId);

            if (creditCard == null)
                return View("Error");

            if (creditCard.CustId != customer.CustId)
                return View("Error");





            _storeContext.CreditCards.Remove(creditCard);
            _storeContext.SaveChanges();
            TempData["Message"] = card.CardName + " Deleted";
            return RedirectToAction("Index", "CreditCard");
        }
    }
}
