using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShoppingWeb;
using ShoppingWeb.Filters;
using ShoppingWeb.Repository;
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
        public void RepeatLogin()
        {
            

        }
    }
}
