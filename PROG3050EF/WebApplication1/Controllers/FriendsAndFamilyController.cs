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
using Microsoft.EntityFrameworkCore;

namespace GameStore.Controllers
{
    public class FriendsAndFamilyController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        public FriendsAndFamilyController(UserManager<User> userMngr, SignInManager<User> signInMngr, StoreContext storeContext)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _storeContext = storeContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Search()
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            List<Customer> userList = new List<Customer>();

            return View(userList);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Search(string? username)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);
            ViewBag.UserId = customer.CustId;

            List<Customer> userList = new List<Customer>();


            if (!String.IsNullOrEmpty(username))
            {
                username = username.Trim();
                userList = _storeContext.Customers.Where(item => item.Nickname.Contains(username) && item.CustId != customer.CustId).ToList();

                ViewBag.ffList = _storeContext.FriendsFamilies.Where(item => item.CustId1 == customer.CustId || item.CustId2 == customer.CustId).ToList();
            }


            return View(userList);
        }

        //accept
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AcceptFamilyRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == id 
            && item.CustId2 == customer.CustId 
            && item.IsFamily == true
            && item.Status == false).FirstOrDefault();

            if (ff != null)
            {
                ff.Status = true;
                ff.DateConnected = DateTime.Now;

                _storeContext.FriendsFamilies.Update(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AcceptFriendRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == id 
            && item.CustId2 == customer.CustId 
            && item.IsFamily == false
            && item.Status == false).FirstOrDefault();

            if (ff != null)
            {
                ff.Status = true;
                ff.DateConnected = DateTime.Now;

                _storeContext.FriendsFamilies.Update(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }


        //decline 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeclineFamilyRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == id 
            && item.CustId2 == customer.CustId 
            && item.IsFamily == true
            && item.Status == false).FirstOrDefault();

            if (ff != null)
            {
                _storeContext.FriendsFamilies.Remove(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeclineFriendRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == id 
            && item.CustId2 == customer.CustId 
            && item.IsFamily == false
            && item.Status == false).FirstOrDefault();

            if (ff != null)
            {
                _storeContext.FriendsFamilies.Remove(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }


        // remove 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFamilyMember(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == id 
            && item.CustId2 == customer.CustId 
            && item.IsFamily == true 
            && item.Status == true).FirstOrDefault();

            if(ff == null)
            {
            ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == customer.CustId
            && item.CustId2 == id
            && item.IsFamily == true
            && item.Status == true).FirstOrDefault();
            }

            if (ff != null)
            {
                _storeContext.FriendsFamilies.Remove(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFriend(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == id
            && item.CustId2 == customer.CustId
            && item.IsFamily == false
            && item.Status == true).FirstOrDefault();

            if (ff == null)
            {
                ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == customer.CustId
                && item.CustId2 == id
                && item.IsFamily == false
                && item.Status == true).FirstOrDefault();
            }


            if (ff != null)
            {
                _storeContext.FriendsFamilies.Remove(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }

        // cancel
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CancelFamilyRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == customer.CustId
            && item.CustId2 == id
            && item.IsFamily == true
            && item.Status == false).FirstOrDefault();

            if (ff != null)
            {
                _storeContext.FriendsFamilies.Remove(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CancelFriendRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = _storeContext.FriendsFamilies.Where(item => item.CustId1 == customer.CustId
            && item.CustId2 == id
            && item.IsFamily == false
            && item.Status == false).FirstOrDefault();

            if (ff != null)
            {
                _storeContext.FriendsFamilies.Remove(ff);
                _storeContext.SaveChanges();
            }

            return RedirectToAction("Search");
        }


        // Send 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SendFamilyRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = new FriendsFamily();

            ff.CustId1 = customer.CustId;
            ff.CustId2 = id;
            ff.Status = false;
            ff.IsFamily = true;
            ff.DateConnected = DateTime.Now;

            _storeContext.FriendsFamilies.Add(ff);
            _storeContext.SaveChanges();
            

            return RedirectToAction("Search");
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SendFriendRequest(int id)
        {
            string currentUsername = User.Identity.Name;
            Customer customer = _storeContext.Customers.SingleOrDefault(cust => cust.Nickname == currentUsername);

            FriendsFamily ff = new FriendsFamily();

            ff.CustId1 = customer.CustId;
            ff.CustId2 = id;
            ff.Status = false;
            ff.IsFamily = false;
            ff.DateConnected = DateTime.Now;

            _storeContext.FriendsFamilies.Add(ff);
            _storeContext.SaveChanges();


            return RedirectToAction("Search");
        }
    }
}
