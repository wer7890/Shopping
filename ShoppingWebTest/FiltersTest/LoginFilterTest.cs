using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingWeb;
using ShoppingWeb.Filters;
using ShoppingWeb.Repository;
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
        public void RepeatLogin()
        {
            _repo.Setup(x => x.RepeatLogin()).Returns((null, 0));

            _privateObject.SetFieldOrProperty("_userRepo", _repo.Object);

            //var result = _loginFilter.OnAuthorization(HttpActionContext e);
            //_repo.Verify(x => x.RepeatLogin(), Times.Once);
            //Assert.AreEqual(result.Status, ActionResult.Success);
        }
    }
}
