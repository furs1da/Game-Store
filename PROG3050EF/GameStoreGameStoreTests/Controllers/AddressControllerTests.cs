using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameStore.Data;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Controllers.Tests
{
    [TestClass()]
    public class AddressControllerTests
    {

        // Need to get data to initiallize controller
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;

        [TestMethod()]
        public void MailingAddressTest(){
            // Arrange
            AddressController controller = new AddressController(userManager, signInManager, _storeContext);

            // Act
            ViewResult result = controller.MailingAddress() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ShippingAddressTest()
        {
            // Arrange
            AddressController controller = new AddressController(userManager, signInManager, _storeContext);

            // Act
            ViewResult result = controller.ShippingAddress() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


 
        [TestMethod()]
        public void EditMailingAddress()
        {
            // Arrange
            AddressController controller = new AddressController(userManager, signInManager, _storeContext);

            // Act
            ViewResult result = controller.EditMailingAddress() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}