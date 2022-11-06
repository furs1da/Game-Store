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
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        public ProfileController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;  
            _storeContext = storeContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileInformation()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (customer == null)
            {
                return View("Error");
            }

            ProfileViewModel vm = new ProfileViewModel();

            vm.CustomerId = customer.CustId;
            vm.FirstName = customer.FirstName != null ? customer.FirstName : null;
            vm.LastName = customer.LastName != null ? customer.LastName : null;
            vm.DOB = customer.Dob != null ? customer.Dob : new DateTime();
            vm.RecievePromotion = (bool)(customer.RecievePromotion != null ? customer.RecievePromotion : false);
            vm.Gender = customer.Gender != null ? customer.Gender : null;

            return View(vm);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileInformationEdit()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (customer == null)
            {
                return View("Error");
            }
            
            ProfileViewModel vm = new ProfileViewModel();

            vm.CustomerId = customer.CustId;
            vm.FirstName = customer.FirstName != null ? customer.FirstName : "";
            vm.LastName = customer.LastName != null ? customer.LastName : "";
            vm.DOB = customer.Dob != null ? customer.Dob : new DateTime(1991, 1, 1);
            vm.RecievePromotion = (bool)(customer.RecievePromotion != null ? customer.RecievePromotion : false);
            vm.Gender = customer.Gender != null ? customer.Gender : 0;

            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileInformationEdit(ProfileViewModel vm)
        {
            var customer = _storeContext.Customers.FirstOrDefault(item => item.CustId == vm.CustomerId);
            if(customer == null)
            {
                return View("Error");
            }

            if (ModelState.IsValid)
            {
                customer.FirstName = vm.FirstName != null ? vm.FirstName : customer.FirstName;
                customer.LastName = vm.LastName != null ? vm.LastName : customer.LastName;
                customer.Dob = vm.DOB != new DateTime() ? vm.DOB : customer.Dob;
                customer.Gender = vm.Gender != 0 ? vm.Gender : customer.Gender;
                customer.RecievePromotion = vm.RecievePromotion;

                _storeContext.Customers.Update(customer);
                _storeContext.SaveChanges();

                return View("ProfileInformation", vm);
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePasswordConfirmation()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            return View();
        }
    }
}
