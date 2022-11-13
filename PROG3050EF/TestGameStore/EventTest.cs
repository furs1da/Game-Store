using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GameStore.Controllers;
using GameStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;

namespace TestGameStore
{
    internal class EventTest
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Event> data { get; set; }
        [Test]
        public void DetailTest()
        {
            var controller = new EventController(userManager, signInManager, _storeContext, data);

            var result = controller.Details(0);

            Assert.IsNotNull(result);
        }
        [Test]
        public void EditEvent()
        {

        }
        [Test]
        public void DeleteEvent()
        {

        }
    }
}