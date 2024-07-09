using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingWeb;
using ShoppingWeb.Filters;
using ShoppingWeb.Repository;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace ShoppingWebTest.FiltersTest
{
    [TestClass]
    public class LoginFilterTest
    {
        private readonly LoginFilter _loginFilter;
        private readonly Mock<IBaseRepository> _repo;
        private readonly PrivateObject _privateObject;

        public LoginFilterTest()
        {
            _loginFilter = new LoginFilter();
            _repo = new Mock<IBaseRepository>();
            _privateObject = new PrivateObject(_loginFilter);
        }

        /// <summary>
        /// RepeatLogin有重複登入 
        /// </summary>
        [TestMethod]
        public void RepeatLoginDuplicateLogin()
        {
            _repo.Setup(x => x.RepeatLogin()).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_baseRepo", _repo.Object);

            var result = _loginFilter.IsRepeatLogin();
            _repo.Verify(x => x.RepeatLogin(), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.DuplicateLogin);
        }

        /// <summary>
        /// RepeatLogin無重複登入 
        /// </summary>
        [TestMethod]
        public void NotRepeatLoginCorrect()
        {
            _repo.Setup(x => x.RepeatLogin()).Returns((null, 1));

            _privateObject.SetFieldOrProperty("_baseRepo", _repo.Object);

            var result = _loginFilter.IsRepeatLogin();
            _repo.Verify(x => x.RepeatLogin(), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.LoginCorrect);
        }

        /// <summary>
        /// RepeatLogin例外 
        /// </summary>
        [TestMethod]
        public void RepeatLoginException()
        {
            _repo.Setup(x => x.RepeatLogin()).Returns((new Exception("RepeatLogin單元測試"), null));
            _repo.Setup(x => x.SetNLog(It.IsAny<Exception>()));

            _privateObject.SetFieldOrProperty("_baseRepo", _repo.Object);

            var result = _loginFilter.IsRepeatLogin();
            _repo.Verify(x => x.RepeatLogin(), Times.Once);
            _repo.Verify(x => x.SetNLog(It.IsAny<Exception>()), Times.Once);
            Assert.AreEqual(result.Status, ActionResult.Error);
        }
    }
}
