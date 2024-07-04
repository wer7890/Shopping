using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingWeb.Repository;
using ShoppingWeb.Controller;
using ShoppingWeb;
using System;
using Moq;
using System.Collections.Generic;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserRepository> _repo;
        private readonly PrivateObject _privateObject;

        public UserControllerTest()
        {
            _userController = new UserController();
            _repo = new Mock<IUserRepository>();
            _privateObject = new PrivateObject(_userController);
        }

        /// <summary>
        /// 帳號資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> UserAccountData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string account = new string('a', i);
                ActionResult expected = (i >= 6 && i <= 16) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { account, expected };
            }
        }

        /// <summary>
        /// 密碼資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> UserPwdData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string pwd = new string('a', i);
                ActionResult expected = (i >= 6 && i <= 16) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pwd, expected };
            }
        }

        /// <summary>
        /// 身分資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> UserRolesData()
        {
            for (int i = 1; i <= 10; i++)
            {
                int roles = i;
                ActionResult expected = (i >= 1 && i <= 3) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { roles, expected };
            }
        }

        /// <summary>
        /// 新增管理員成功
        /// </summary>
        [TestMethod]
        public void AddUserSuccess()
        {
            _repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            AddUserDto addUserDto = new AddUserDto
            {
                Account = "test11",
                Pwd = "123456",
                Roles = 1
            };
            var result = _userController.AddUser(addUserDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }


        /// <summary>
        /// 新增管理員失敗
        /// </summary>
        [TestMethod]
        public void AddUserFailure()
        {
            _repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            AddUserDto addUserDto = new AddUserDto
            {
                Account = "test11",
                Pwd = "123456",
                Roles = 1
            };
            var result = _userController.AddUser(addUserDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }


        /// <summary>
        /// 帳號參數判斷
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserAccountData), DynamicDataSourceType.Method)]  //DynamicData用來指定測試方法所需的測試數據將動態生成。GenerateUserAccountData 方法生成一組測試數據，這些數據將傳遞給測試方法 
        public void AddUserAccountInput(string account, ActionResult expected)
        {
            _repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            AddUserDto addUserDto = new AddUserDto
            {
                Account = account,
                Pwd = "123456",
                Roles = 1
            };
            var result = _userController.AddUser(addUserDto);

            Console.WriteLine(account);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// 密碼參數判斷
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserPwdData), DynamicDataSourceType.Method)]
        public void AddUserPwdInput(string pwd, ActionResult expected)
        {
            _repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            AddUserDto addUserDto = new AddUserDto
            {
                Account = "test11",
                Pwd = pwd,
                Roles = 1
            };
            var result = _userController.AddUser(addUserDto);

            Console.WriteLine(pwd);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// 身分參數判斷
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserRolesData), DynamicDataSourceType.Method)]
        public void AddUserRolesInput(int roles, ActionResult expected)
        {
            _repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            AddUserDto addUserDto = new AddUserDto
            {
                Account = "test11",
                Pwd = "123456",
                Roles = roles
            };
            var result = _userController.AddUser(addUserDto);

            Console.WriteLine(roles);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, expected);
        }










        /// <summary>
        /// AddUser參數判斷失敗
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <param name="roles"></param>
        /// <param name="res"></param>
        //[DataTestMethod]
        //[DataRow("test", "123456", 2, ActionResult.InputError)]  //最後的參數為預期的結果
        //[DataRow("test11", "1234", 2, ActionResult.InputError)]
        //[DataRow("test11", "123456", 4, ActionResult.InputError)]
        //public void AddUserInputFailure(string account, string pwd, int roles, ActionResult res)
        //{
        //    _repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));

        //    _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

        //    AddUserDto addUserDto = new AddUserDto
        //    {
        //        Account = account,
        //        Pwd = pwd,
        //        Roles = roles
        //    };
        //    var result = _userController.AddUser(addUserDto);
        //    Console.WriteLine(result.Status);
        //    Console.WriteLine(res);
        //    Assert.AreEqual(result.Status, res);
        //}






        /// <summary>
        /// 刪除管理員成功
        /// </summary>
        //[TestMethod]
        //public void DelUserInfoSuccess()
        //{
        //    var _userConntroller = new UserController();
        //    Mock<IUserRepository> repo = new Mock<IUserRepository>();
        //    repo.Setup(x => x.DelUserInfo(It.IsAny<DelUserInfoDto>())).Returns((null, 1));  //塞假資料

        //    PrivateObject privateObject = new PrivateObject(_userConntroller);  //使用虛擬的對象
        //    privateObject.SetFieldOrProperty("_userRepo", repo.Object);

        //    DelUserInfoDto delUserInfoDto = new DelUserInfoDto();
        //    var result = _userConntroller.DelUserInfo(delUserInfoDto);
        //    Console.WriteLine(result.Status);
        //    Assert.AreEqual(result.Status, ActionResult.Success);
        //}




        /// <summary>
        /// 新增管理員成功
        /// </summary>
        //[TestMethod]
        //public void AddUserSuccess()
        //{
        //    var _userController = new UserController();
        //    Mock<IUserRepository> repo = new Mock<IUserRepository>();  //IUserRepository是Mock的一個介面，使用Moq來創建一個IUserRepository的mock 對象。這樣可以模擬 IUserRepository 的行為，而不需要依賴實際的數據庫操作
        //    repo.Setup(x => x.AddUser(It.IsAny<AddUserDto>())).Returns((null, 1));  //設置mock對象的AddUser方法，AddUser是其內部定義的方法，<AddUserDto>為調用該方法時的參數類型，(null, 1)為調用該方法時返回的值

        //    PrivateObject privateObject = new PrivateObject(_userController);  //創建 PrivateObject 的實例，這是一個用於測試私有成員或方法的輔助類。此處用於訪問 UserController 的私有成員 _userRepo
        //    privateObject.SetFieldOrProperty("_userRepo", repo.Object);  //測試中的UserController使用的IUserRepository實例將是模擬的版本，而不是實際的 UserRepository，repo.Object 返回這個模擬對象的實例

        //    AddUserDto addUserDto = new AddUserDto  //創建AddUser中所帶入的參數
        //    {
        //        Account = "test11",
        //        Pwd = "123456",
        //        Roles = 1
        //    };
        //    var result = _userController.AddUser(addUserDto);  //調用UserController中的AddUser方法
        //    Console.WriteLine(result.Status);
        //    Assert.AreEqual(result.Status, ActionResult.Success);
        //}
    }
}
