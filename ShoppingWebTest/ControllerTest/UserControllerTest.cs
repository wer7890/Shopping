using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingWeb.Repository;
using ShoppingWeb.Controller;
using ShoppingWeb;
using System;
using Moq;
using System.Collections.Generic;
using System.Data;

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
            for (int i = -10; i <= 10; i++)
            {
                int roles = i;
                ActionResult expected = (i >= 1 && i <= 3) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { roles, expected };
            }
        }

        /// <summary>
        /// ID資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> UserIdData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int id = i;
                ActionResult expected = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { id, expected };
            }
        }

        /// <summary>
        /// PageNumber資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> PageNumberData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int pageNumber = i;
                ActionResult expected = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pageNumber, expected };
            }
        }

        /// <summary>
        /// PageSize資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> PageSizeData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int pageSize = i;
                ActionResult expected = (i >= 1 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pageSize, expected };
            }
        }

        /// <summary>
        /// BeforePagesTotal資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> BeforePagesTotalData()
        {
            for (int i = -10; i <= 10; i++)
            {
                int beforePagesTotal = i;
                ActionResult expected = (i >= 0 && i <= int.MaxValue) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { beforePagesTotal, expected };
            }
        }



        /// <summary>
        /// AddUser成功
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
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// AddUser失敗
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
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// AddUser帳號長度判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserAccountData), DynamicDataSourceType.Method)]  //DynamicData用來指定測試方法所需的測試數據將動態生成。GenerateUserAccountData 方法生成一組測試數據，這些數據將傳遞給測試方法 
        public void AddUserAccountLength(string account, ActionResult expected)
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
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// AddUser帳號特殊符號判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("test11", ActionResult.Success)]
        [DataRow("test11-", ActionResult.InputError)]
        public void AddUserAccountSpecial(string account, ActionResult res)
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
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddUser密碼長度判斷
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserPwdData), DynamicDataSourceType.Method)]
        public void AddUserPwdLength(string pwd, ActionResult expected)
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
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// AddUser密碼特殊符號判斷
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("123456", ActionResult.Success)]
        [DataRow("123456+", ActionResult.InputError)]
        public void AddUserPwdSpecial(string pwd, ActionResult res)
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
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// AddUser身分判斷
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserRolesData), DynamicDataSourceType.Method)]
        public void AddUserRolesRange(int roles, ActionResult expected)
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
            Assert.AreEqual(result.Status, expected);
        }



        /// <summary>
        /// DelUserInfo成功
        /// </summary>
        [TestMethod]
        public void DelUserInfoSuccess()
        {
            _repo.Setup(x => x.DelUserInfo(It.IsAny<DelUserInfoDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            DelUserInfoDto delUserInfoDto = new DelUserInfoDto
            {
                UserId = 1
            };
            var result = _userController.DelUserInfo(delUserInfoDto);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// DelUserInfo失敗
        /// </summary>
        [TestMethod]
        public void DelUserInfoFailure()
        {
            _repo.Setup(x => x.DelUserInfo(It.IsAny<DelUserInfoDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            DelUserInfoDto delUserInfoDto = new DelUserInfoDto
            {
                UserId = 1
            };
            var result = _userController.DelUserInfo(delUserInfoDto);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// DelUserInfo id判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserIdData), DynamicDataSourceType.Method)]
        public void DelUserInfoIdInput(int id, ActionResult expected)
        {
            _repo.Setup(x => x.DelUserInfo(It.IsAny<DelUserInfoDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            DelUserInfoDto delUserInfoDto = new DelUserInfoDto
            {
                UserId = id
            };
            var result = _userController.DelUserInfo(delUserInfoDto);
            Assert.AreEqual(result.Status, expected);
        }



        /// <summary>
        /// EditUser成功
        /// </summary>
        [TestMethod]
        public void EditUserSuccess()
        {
            _repo.Setup(x => x.EditUser(It.IsAny<EditUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditUserDto editUserDto = new EditUserDto
            {
                Pwd = "123456"
            };
            var result = _userController.EditUser(editUserDto);

            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditUser失敗
        /// </summary>
        [TestMethod]
        public void EditUserFailure()
        {
            _repo.Setup(x => x.EditUser(It.IsAny<EditUserDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditUserDto editUserDto = new EditUserDto
            {
                Pwd = "123456"
            };
            var result = _userController.EditUser(editUserDto);

            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditUser密碼長度判斷
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserPwdData), DynamicDataSourceType.Method)]
        public void EditUserPwdLength(string pwd, ActionResult expected)
        {
            _repo.Setup(x => x.EditUser(It.IsAny<EditUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditUserDto editUserDto = new EditUserDto
            {
                Pwd = pwd
            };
            var result = _userController.EditUser(editUserDto);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// EditUser密碼特殊符號判斷
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DataRow("123456", ActionResult.Success)]
        [DataRow("123456+", ActionResult.InputError)]
        public void EditUserPwdSpecial(string pwd, ActionResult expected)
        {
            _repo.Setup(x => x.EditUser(It.IsAny<EditUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditUserDto editUserDto = new EditUserDto
            {
                Pwd = pwd
            };
            var result = _userController.EditUser(editUserDto);
            Assert.AreEqual(result.Status, expected);
        }



        /// <summary>
        /// EditUserRoles成功
        /// </summary>
        [TestMethod]
        public void EditUserRolesSuccess()
        {
            _repo.Setup(x => x.EditUserRoles(It.IsAny<EditRolesDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditRolesDto editRolesDto = new EditRolesDto
            {
                UserId = 1,
                Roles = 1
            };
            var result = _userController.EditUserRoles(editRolesDto);

            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// EditUserRoles失敗
        /// </summary>
        [TestMethod]
        public void EditUserRolesFailure()
        {
            _repo.Setup(x => x.EditUserRoles(It.IsAny<EditRolesDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditRolesDto editRolesDto = new EditRolesDto
            {
                UserId = 1,
                Roles = 1
            };
            var result = _userController.EditUserRoles(editRolesDto);

            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// EditUserRoles ID判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserIdData), DynamicDataSourceType.Method)]
        public void EditUserRolesIdInput(int id, ActionResult expected)
        {
            _repo.Setup(x => x.EditUserRoles(It.IsAny<EditRolesDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditRolesDto editRolesDto = new EditRolesDto
            {
                UserId = id,
                Roles = 1
            };
            var result = _userController.EditUserRoles(editRolesDto);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// EditUserRoles身分判斷
        /// </summary>
        /// <param name="roles"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserRolesData), DynamicDataSourceType.Method)]
        public void EditUserRolesRange(int roles, ActionResult expected)
        {
            _repo.Setup(x => x.EditUserRoles(It.IsAny<EditRolesDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            EditRolesDto editRolesDto = new EditRolesDto
            {
                UserId = 1,
                Roles = roles
            };
            var result = _userController.EditUserRoles(editRolesDto);
            Assert.AreEqual(result.Status, expected);
        }



        /// <summary>
        /// SetSessionSelectUserId成功
        /// </summary>
        [TestMethod]
        public void SetSessionSelectUserIdSuccess()
        {
            _repo.Setup(x => x.SetSessionSelectUserId(It.IsAny<SetSessionSelectUserIdDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            SetSessionSelectUserIdDto setSessionSelectUserIdDto = new SetSessionSelectUserIdDto
            {
                UserId = 1,
            };
            var result = _userController.SetSessionSelectUserId(setSessionSelectUserIdDto);

            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// SetSessionSelectUserId失敗
        /// </summary>
        [TestMethod]
        public void SetSessionSelectUserIdFailure()
        {
            _repo.Setup(x => x.SetSessionSelectUserId(It.IsAny<SetSessionSelectUserIdDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            SetSessionSelectUserIdDto setSessionSelectUserIdDto = new SetSessionSelectUserIdDto
            {
                UserId = 1,
            };
            var result = _userController.SetSessionSelectUserId(setSessionSelectUserIdDto);

            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// SetSessionSelectUserId ID判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(UserIdData), DynamicDataSourceType.Method)]
        public void SetSessionSelectUserIdInput(int id, ActionResult expected)
        {
            _repo.Setup(x => x.SetSessionSelectUserId(It.IsAny<SetSessionSelectUserIdDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            SetSessionSelectUserIdDto setSessionSelectUserIdDto = new SetSessionSelectUserIdDto
            {
                UserId = id,
            };
            var result = _userController.SetSessionSelectUserId(setSessionSelectUserIdDto);
            Assert.AreEqual(result.Status, expected);
        }



        /// <summary>
        /// GetAllUserData成功
        /// </summary>
        [TestMethod]
        public void GetAllUserDataSuccess()
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_roles", typeof(byte));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_roles"] = 1;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllUserData(It.IsAny<GetAllUserDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            GetAllUserDataDto getAllUserDataDto = new GetAllUserDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _userController.GetAllUserData(getAllUserDataDto);
            Console.WriteLine(result.Status);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// GetAllUserData失敗
        /// </summary>
        [TestMethod]
        public void GetAllUserDataFailure()
        {
            _repo.Setup(x => x.GetAllUserData(It.IsAny<GetAllUserDataDto>())).Returns((null, 0, null));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            GetAllUserDataDto getAllUserDataDto = new GetAllUserDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _userController.GetAllUserData(getAllUserDataDto);

            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// GetAllUserData的PageNumber參數判斷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageNumberData), DynamicDataSourceType.Method)]
        public void GetAllUserDataPageNumberInput(int pageNumber, ActionResult expected)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_roles", typeof(byte));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_roles"] = 1;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllUserData(It.IsAny<GetAllUserDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            GetAllUserDataDto getAllUserDataDto = new GetAllUserDataDto
            {
                PageNumber = pageNumber,
                PageSize = 1,
                BeforePagesTotal = 1
            };
            var result = _userController.GetAllUserData(getAllUserDataDto);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// GetAllUserData的PageSize參數判斷
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageSizeData), DynamicDataSourceType.Method)]
        public void GetAllUserDataPageSizeInput(int pageSize, ActionResult expected)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_roles", typeof(byte));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_roles"] = 1;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllUserData(It.IsAny<GetAllUserDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            GetAllUserDataDto getAllUserDataDto = new GetAllUserDataDto
            {
                PageNumber = 1,
                PageSize = pageSize,
                BeforePagesTotal = 1
            };
            var result = _userController.GetAllUserData(getAllUserDataDto);
            Assert.AreEqual(result.Status, expected);
        }

        /// <summary>
        /// GetAllUserData的BeforePagesTotal參數判斷
        /// </summary>
        /// <param name="beforePagesTotal"></param>
        /// <param name="expected"></param>
        [DataTestMethod]
        [DynamicData(nameof(PageSizeData), DynamicDataSourceType.Method)]
        public void GetAllUserDataBeforePagesTotalInput(int beforePagesTotal, ActionResult expected)
        {
            DataTable dt = new DataTable("Test");
            dt.Columns.Add("f_id", typeof(int));
            dt.Columns.Add("f_account", typeof(string));
            dt.Columns.Add("f_roles", typeof(byte));
            DataRow row = dt.NewRow();
            row["f_id"] = 1;
            row["f_account"] = "test11";
            row["f_roles"] = 1;
            dt.Rows.Add(row);

            _repo.Setup(x => x.GetAllUserData(It.IsAny<GetAllUserDataDto>())).Returns((null, 1, dt));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            GetAllUserDataDto getAllUserDataDto = new GetAllUserDataDto
            {
                PageNumber = 1,
                PageSize = 1,
                BeforePagesTotal = beforePagesTotal
            };
            var result = _userController.GetAllUserData(getAllUserDataDto);
            Assert.AreEqual(result.Status, expected);
        }







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
