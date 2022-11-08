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
    public class AddressController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        public AddressController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
        }


        [Authorize]
        [HttpGet]
        public IActionResult ShippingAddress()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (customer == null)
            {
                return View("Error");
            }


            AddressViewModel vm = new AddressViewModel();

            ShippingAddress shippingAddress = _storeContext.ShippingAddresses.SingleOrDefault(addr => addr.ShippingAddressId == customer.ShippingAddressId);
            
            if(shippingAddress != null)
            {
                vm.CustomerId = customer.CustId;
                vm.Address = shippingAddress.Address != null ? shippingAddress.Address : null;
                vm.City = shippingAddress.City != null ? shippingAddress.City : null;
                vm.Province = shippingAddress.Province != null ? shippingAddress.Province : 0;
                vm.PostalCode = shippingAddress.PostalCode != null ? shippingAddress.PostalCode : null;
            }
            else
            {
                vm.CustomerId = customer.CustId;
                vm.Address = null;
                vm.City = null;
                vm.Province = 0;
                vm.PostalCode = null;
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult MailingAddress()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            if (customer == null)
            {
                return View("Error");
            }


            AddressViewModel vm = new AddressViewModel();

            MailingAddress mailingAddress = _storeContext.MailingAddresses.SingleOrDefault(addr => addr.MailingAddressId == customer.MailingAddressId);

            if (mailingAddress != null)
            {
                vm.CustomerId = customer.CustId;
                vm.Address = mailingAddress.Address != null ? mailingAddress.Address : null;
                vm.City = mailingAddress.City != null ? mailingAddress.City : null;
                vm.Province = mailingAddress.Province != null ? mailingAddress.Province : 0;
                vm.PostalCode = mailingAddress.PostalCode != null ? mailingAddress.PostalCode : null;
            }
            else
            {
                vm.CustomerId = customer.CustId;
                vm.Address = null;
                vm.City = null;
                vm.Province = 0;
                vm.PostalCode = null;
            }

            return View(vm);
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditMailingAddress()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditShippingAddress()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditMailingAddress(AddressViewModel vm)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditShippingAddress(AddressViewModel vm)
        {
            return View();
        }
    }
}
