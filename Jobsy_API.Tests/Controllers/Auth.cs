using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Http;
using System.Threading.Tasks;
using ENTITY_L.Models.Authentication;
using Jobsy_API.Controllers;

namespace Jobsy_API.Tests.Controllers
{
    [TestClass]
    public class Auth
    {
        AuthenticationAPIController authh = new AuthenticationAPIController();

        [TestMethod()]
        public async void Login()
        {
            LoginModel loginmodel = new LoginModel();
            loginmodel.Email = "Randy140801@gmail.com";
            loginmodel.Password = "ranranran";

            LoginModel auth = await authh.Login(loginmodel);

            Assert.AreEqual(auth, loginmodel);
        }
    }
}
