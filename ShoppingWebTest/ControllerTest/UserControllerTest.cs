using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingWeb.Repository;
using ShoppingWeb.Controller;
using ShoppingWeb;
using System;
using Moq;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class UserControllerTest
    {

        /// <summary>
        /// 新增管理員成功
        /// </summary>
        [TestMethod]
        public void AddUserSuccess()
        {
            var _userController = new UserController();
            Mock<IUserRepository> repo = new Mock<IUserRepository>();  //IUserRepository是Mock的一個介面，使用Moq來創建一個IUserRepository的mock 對象。這樣可以模擬 IUserRepository 的行為，而不需要依賴實際的數據庫操作
            repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));  //設置mock對象的AddUser方法，AddUser是其內部定義的方法，<AddUserDto>為調用該方法時的參數類型，(null, 1)為調用該方法時返回的值

            PrivateObject privateObject = new PrivateObject(_userController);  //創建 PrivateObject 的實例，這是一個用於測試私有成員或方法的輔助類。此處用於訪問 UserController 的私有成員 _userRepo
            privateObject.SetFieldOrProperty("_userRepo", repo.Object);  //測試中的UserController使用的IUserRepository實例將是模擬的版本，而不是實際的 UserRepository，repo.Object 返回這個模擬對象的實例

            AddUserDto addUserDto = new AddUserDto();  //創建AddUser中所帶入的參數
            var result = _userController.AddUser(addUserDto);  //調用UserController中的AddUser方法
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// 新增管理員失敗
        /// </summary>
        [TestMethod]
        public void AddUserFailure()
        {
            var _userController = new UserController();
            Mock<IUserRepository> repo = new Mock<IUserRepository>();
            repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 0));

            PrivateObject privateObject = new PrivateObject(_userController);
            privateObject.SetFieldOrProperty("_userRepo", repo.Object);

            AddUserDto addUserDto = new AddUserDto();
            var result = _userController.AddUser(addUserDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }


        /// <summary>
        /// 刪除管理員成功
        /// </summary>
        [TestMethod]
        public void DelUserInfoSuccess()
        {
            var _userConntroller = new UserController();
            Mock<IUserRepository> repo = new Mock<IUserRepository>();
            repo.Setup(x => x.DelUserInfo(It.IsAny<DelUserInfoDto>())).Returns((null, 1));  //塞假資料

            PrivateObject privateObject = new PrivateObject(_userConntroller);  //使用虛擬的對象
            privateObject.SetFieldOrProperty("_userRepo", repo.Object);

            DelUserInfoDto delUserInfoDto = new DelUserInfoDto();
            var result = _userConntroller.DelUserInfo(delUserInfoDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }
    }
}
