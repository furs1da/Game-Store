using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GameStore.Models.Repositories;

namespace GameStore.Controllers.Tests
{
    [TestClass()]
    public class EventControllerTests
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Event> data { get; set; }


        [TestMethod()]
        public void DetailsTest()
        {
            // Arrange
            EventController controller = new EventController(userManager, signInManager, _storeContext,data);

            // Act
            ViewResult result = controller.Details(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void MyDetailsTest()
        {
            // Arrange
            EventController controller = new EventController(userManager, signInManager, _storeContext, data);

            // Act
            ViewResult result = controller.MyDetails(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}