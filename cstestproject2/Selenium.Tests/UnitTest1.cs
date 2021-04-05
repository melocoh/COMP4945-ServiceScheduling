using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "https://localhost:44334";
            ChromeDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.LinkText("Start Now")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            
            // Select Your Role
            driver.FindElement(By.LinkText("Client")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Register as a new user
            driver.FindElement(By.LinkText("Register as a new user")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.Id("loginfullname")).SendKeys("Jason Lee");
            driver.FindElement(By.Id("loginemail")).SendKeys("jasoncflee@bcit.ca");
            driver.FindElement(By.Id("loginpassword")).SendKeys("A123s456*");
            driver.FindElement(By.Id("confirmloginpassword")).SendKeys("A123s456*");

            // Client Log In
            driver.FindElement(By.LinkText("Already a user?")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.Id("loginemail")).SendKeys("jasoncflee@bcit.ca");
            driver.FindElement(By.Id("loginpassword")).SendKeys("A123s456*");

            // Select Your Role
            driver.FindElement(By.LinkText("Home")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.LinkText("Start Now")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.LinkText("Employee")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Employee Registration
            driver.FindElement(By.LinkText("Register as a new user")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.Id("loginfullname")).SendKeys("Jason Lee");

            var jobTitle = driver.FindElement(By.Name("loginjobtitle"));
            var selectJobTitle = new SelectElement(jobTitle);
            selectJobTitle.SelectByValue("Fire Fighter");

            var certification = driver.FindElement(By.Name("logincertification"));
            var selectCertification = new SelectElement(certification);
            selectCertification.SelectByValue("Scuba Diving Certification");

            //driver.FindElement(By.Id("loginjobtitle")).SendKeys("CST Instructor");
            //driver.FindElement(By.Id("certificationType")).SendKeys("");
            driver.FindElement(By.Id("loginemail")).SendKeys("jasoncflee@bcit.ca");
            driver.FindElement(By.Id("loginpassword")).SendKeys("A123s456*");
            driver.FindElement(By.Id("confirmloginpassword")).SendKeys("A123s456*");

            // Employee Log In
            driver.FindElement(By.LinkText("Already a user?")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.Id("loginemail")).SendKeys("jasoncflee@bcit.ca");
            driver.FindElement(By.Id("loginpassword")).SendKeys("A123s456*");

            // Back to Home
            driver.FindElement(By.LinkText("Home")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Submit
            //driver.FindElement(By.XPath("//Input[@type='submit']")).Click();
        }
    }
}
