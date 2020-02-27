//UnitTest1 is testing textboxes for login screen.
// assert statements should test if password and email fields are not empty.
// if input password or email are null then the test fails

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using CapgeminiElections.Areas.Identity.Pages.Account;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {

            LoginModel.InputModel login = new LoginModel.InputModel();
            login.Password = "Example Password";
            Assert.IsNotNull(login.Password);

            login.Email = "Example Email";
            Assert.IsNotNull(login.Email);

        }
    }
}
    