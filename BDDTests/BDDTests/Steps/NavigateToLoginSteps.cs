using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace BDDTests.Steps
{
    [Binding]
    public class NavigateToLoginSteps
    {
        private static IWebDriver driver;

        [Given(@"Im a user on the homepage")]
        public void GivenImAUserOnTheHomepage()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
            driver.Manage().Window.Maximize();
            //website published for tests 
            driver.Navigate().GoToUrl("https://capgeminielection.azurewebsites.net/");
        }
        
        [When(@"I press login in the upper corner")]
        public void WhenIPressLoginInTheUpperCorner()
        {
            //Locate the Web Elements
            driver.FindElement(By.LinkText("Login")).Click();
            Thread.Sleep(5000);

        }
        
        [Then(@"I should navigate to the login page")]
        public void ThenIShouldNavigateToTheLoginPage()
        {
            String URL = driver.Url;
            Assert.IsTrue(URL == "https://capgeminielection.azurewebsites.net/Identity/Account/Login");
            driver.Close();
            driver.Quit();
        }
    }
}
