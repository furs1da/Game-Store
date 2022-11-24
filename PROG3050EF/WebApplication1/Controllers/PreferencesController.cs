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
using GameStore.Models.ViewModels;

namespace GameStore.Controllers
{
    
    public class PreferencesController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        public PreferencesController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Preferences()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (customer == null)
            {
                return View("Error");
            }
            
            PreferencesViewModel vm = new PreferencesViewModel();

            vm.Platforms = _storeContext.Platforms.ToList();
            vm.Categories = _storeContext.Categories.ToList();

            vm.CustomerId = customer.CustId;

            vm.CategoryId = customer.PreferedCategoryId != null ? customer.PreferedCategoryId : null;
            vm.PlatformId = customer.PreferedPlatformId != null ? customer.PreferedPlatformId : null;

            return RedirectToAction("PreferencesEdit");
            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult PreferencesEdit()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (customer == null)
            {
                return View("Error");
            }

            PreferencesViewModel vm = new PreferencesViewModel();

            vm.Platforms = _storeContext.Platforms.ToList();
            vm.Categories = _storeContext.Categories.ToList();

            vm.CustomerId = customer.CustId;

            vm.CategoryId = customer.PreferedCategoryId != null ? customer.PreferedCategoryId : null;
            vm.PlatformId = customer.PreferedPlatformId != null ? customer.PreferedPlatformId : null;

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PreferencesEdit(PreferencesViewModel vm)
        {
            var customer = _storeContext.Customers.FirstOrDefault(item => item.CustId == vm.CustomerId);
            if (customer == null)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                customer.PreferedCategoryId = vm.CategoryId != 0 ? vm.CategoryId : customer.PreferedCategoryId;
                customer.PreferedPlatformId = vm.PlatformId != 0 ? vm.PlatformId : customer.PreferedPlatformId;

                vm.CategoryId = customer.PreferedCategoryId != null ? customer.PreferedCategoryId : null;
                vm.PlatformId = customer.PreferedPlatformId != null ? customer.PreferedPlatformId : null;

                vm.Platforms = _storeContext.Platforms.ToList();
                vm.Categories = _storeContext.Categories.ToList();

                _storeContext.Customers.Update(customer);
                _storeContext.SaveChanges();

                return View("Preferences", vm);
            }
           
            return View("PreferencesEdit", vm);
        }
    }
}
