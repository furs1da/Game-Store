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

namespace GameStore.Controllers.Tests
{
    [TestClass()]
    public class CreditCardControllerTests
    {

        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        private StoreContext _storeContext;




        [TestMethod()]
        public void IndexTest()
        {
            // Arrange
            CreditCardController controller = new CreditCardController(userManager, signInManager, _storeContext);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        public void EditTest()
        {
            // Arrange
            CreditCardController controller = new CreditCardController(userManager, signInManager, _storeContext);

            // Act
            ViewResult result = controller.Edit(0) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}