using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingWeb;
using ShoppingWeb.Controller;
using ShoppingWeb.Repository;
using System;
using System.Collections.Generic;

namespace ShoppingWebTest.ControllerTest
{
    [TestClass]
    public class LoginControllerTest
    {
        private readonly LoginController _loginController;
        private readonly Mock<ILoginRepository> _repo;
        private readonly PrivateObject _privateObject;

        public LoginControllerTest()
        {
            _loginController = new LoginController();
            _repo = new Mock<ILoginRepository>();
            _privateObject = new PrivateObject(_loginController);
        }

        /// <summary>
        /// 帳號資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> AccountData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string account = new string('a', i);
                ActionResult res = (i >= 6 && i <= 16) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { account, res };
            }
        }

        /// <summary>
        /// 密碼資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> PwdData()
        {
            for (int i = 1; i <= 20; i++)
            {
                string pwd = new string('a', i);
                ActionResult res = (i >= 6 && i <= 16) ? ActionResult.Success : ActionResult.InputError;
                yield return new object[] { pwd, res };
            }
        }



        /// <summary>
        /// LoginUser成功
        /// </summary>
        [TestMethod]
        public void LoginUserSuccess()
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = "test11",
                Pwd = "123456"
            };
            var result = _loginController.LoginUser(loginUserDto);
            _repo.Verify(x => x.LoginUser(It.IsAny<LoginUserDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Success);
        }

        /// <summary>
        /// LoginUser失敗
        /// </summary>
        [TestMethod]
        public void LoginUserFailure()
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = "test11",
                Pwd = "123456"
            };
            var result = _loginController.LoginUser(loginUserDto);
            _repo.Verify(x => x.LoginUser(It.IsAny<LoginUserDto>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Failure);
        }

        /// <summary>
        /// LoginUser例外
        /// </summary>
        [TestMethod]
        public void LoginUserException()
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((new Exception("AddUser單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));
            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = "test11",
                Pwd = "123456"
            };
            var result = _loginController.LoginUser(loginUserDto);
            _repo.Verify(x => x.LoginUser(It.IsAny<LoginUserDto>()), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }

        /// <summary>
        /// LoginUser帳號長度判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(AccountData), DynamicDataSourceType.Method)] 
        public void LoginUserAccountLength(string account, ActionResult res)
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = account,
                Pwd = "123456",
            };
            var result = _loginController.LoginUser(loginUserDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// LoginUser帳號特殊符號判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("test11-", ActionResult.InputError)]
        public void LoginUserAccountSpecial(string account, ActionResult res)
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = account,
                Pwd = "123456"
            };
            var result = _loginController.LoginUser(loginUserDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// LoginUser密碼長度判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DynamicData(nameof(PwdData), DynamicDataSourceType.Method)]
        public void LoginUserPwdLength(string pwd, ActionResult res)
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = "test11",
                Pwd = pwd,
            };
            var result = _loginController.LoginUser(loginUserDto);
            Assert.AreEqual(result.Status, res);
        }

        /// <summary>
        /// LoginUser密碼特殊符號判斷
        /// </summary>
        /// <param name="account"></param>
        /// <param name="res"></param>
        [DataTestMethod]
        [DataRow("123456-", ActionResult.InputError)]
        public void LoginUserPwdSpecial(string pwd, ActionResult res)
        {
            _repo.Setup(x => x.LoginUser(It.IsAny<LoginUserDto>())).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_loginRepo", _repo.Object);

            LoginUserDto loginUserDto = new LoginUserDto
            {
                Account = "test11",
                Pwd = pwd
            };
            var result = _loginController.LoginUser(loginUserDto);
            Assert.AreEqual(result.Status, res);
        }
    }
}
