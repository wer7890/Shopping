using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingWeb.Repository;
using ShoppingWeb;
using System;
using Moq;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void AddUserInput()
        {
            AddUserDto addUserDto = new AddUserDto();            
            addUserDto.Account = "tda";
            addUserDto.Pwd = "123456";
            addUserDto.Roles = 3;

            var _userController = new TestRepository();
            Mock<IUserRepository> repo = new Mock<IUserRepository>();
            repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));
            
            PrivateObject privateObject = new PrivateObject(_userController);
            privateObject.SetFieldOrProperty("_userRepo", repo.Object);
                
            var result = _userController.AddUser(addUserDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }
    }
}
