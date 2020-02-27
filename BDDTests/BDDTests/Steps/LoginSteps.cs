using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
//Login Test still work in progress does not work for milestone navigate to login was created in order to replace


namespace BDDTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private static IWebDriver driver;

        [Given(@"I Navigate to the Login page")]
        public void GivenINavigateToTheLoginPage()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
            //website published for tests 
            driver.Navigate().GoToUrl("https://capgeminielection.azurewebsites.net/Identity/Account/Login");
        }

        [When(@"I Login with Username '(.*)' and Password '(.*)' on the Login Page")]
        public void WhenILoginWithUsernameAndPasswordOnTheLoginPage(string username, string password)
        {
            //Locate the Web Elements
            var usernameBox = driver.FindElement(By.Id("Email"));
            var passwordBox = driver.FindElement(By.Id("Password"));
            var submitBtn = driver.FindElement(By.ClassName("btn btn-primary"));

            //Perform Required action with the element
            usernameBox.SendKeys(username);
            Thread.Sleep(500);
            passwordBox.SendKeys(password);
            Thread.Sleep(500);
            submitBtn.Click();
            Thread.Sleep(500);
        }
        [Then(@"logout button should be visable on home page")]
        public void ThenLogoutButtonShouldBeVisableOnHomePage()
        {
            var logout = driver.FindElement(By.Id("logout")).Text;//not sure if this will be the end assert case
            Assert.IsTrue(logout == "logout");
            driver.Close();
            driver.Quit();
        }
    }
}
