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
            var _userController = new UserController();
            Mock<IUserRepository> repo = new Mock<IUserRepository>();  //IUserRepository是Mock的一個介面
            repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));  //AddUser是其內部定義的方法，<AddUserDto>為調用該方法時的參數類型，(null, 1)為調用該方法時返回的值

            PrivateObject privateObject = new PrivateObject(_userController);  //PrivateObject用於實例方法的測試
            privateObject.SetFieldOrProperty("_userRepo", repo.Object);

            AddUserDto addUserDto = new AddUserDto();
            var result = _userController.AddUser(addUserDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }
    }
}
