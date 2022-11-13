using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Data;
using Microsoft.AspNetCore.Identity;
using GameStore.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers.Tests
{
    [TestClass()]
    public class GameControllerTests
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;
        private IRepository<Game> data { get; set; }



        [TestMethod()]
        public void DetailsTest()
        {
            // Arrange
            GameController controller = new GameController(userManager, signInManager, _storeContext,data);

            // Act
            ViewResult result = controller.Details(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}