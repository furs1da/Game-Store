using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Data;
using GameStore.Models.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers.Tests
{
    [TestClass()]
    public class WishListControllerTests
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;




        [TestMethod()]
        public void MyListTest()
        {
            // Arrange
            WishListController controller = new WishListController(userManager, signInManager, _storeContext);

            // Act
            ViewResult result = controller.MyList() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void GameDetailsTest()
        {
            // Arrange
            WishListController controller = new WishListController(userManager, signInManager, _storeContext);
            

            // Act
            ViewResult result = controller.GameDetails(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}