using Backup.Base;
using Backup.Elements;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup.Base
{
    
    class login : BaseClass
    {
        loginPage_POM loginPage;
        [SetUp]
        public void test0()
        {
            loginPage = new loginPage_POM(driver);
        }

        
        [Test]
        [Description("wrong username")]
        public void test1(string s)
        {
            loginPage.setLoginDetails(userName+"1",Password);
            loginPage.acceptTerms();
            loginPage.clickLogin();
            Assert.IsTrue(loginPage.errorDisplayed());
        }      
        
        [Test]
        [Description("Wrong password")]
        public void test2()
        {
            loginPage.setLoginDetails(userName , Password + "1");
            loginPage.acceptTerms();
            loginPage.clickLogin();
            Assert.IsTrue(loginPage.errorDisplayed());
        }

        [Test]
        [Description("Terms and Conditions- Must accept terms")]
        public void test3()
        {
            loginPage.setLoginDetails(userName, Password);
            loginPage.clickLogin();
            Assert.IsTrue(loginPage.TermsErrorDisplayed());
        }
              
        [Test]
        [Description("Terms and Conditions website")]
        public void test4()
        {
            string termsLink = "https://afi.cloud/TermsAndConditions";
            loginPage.setLoginDetails(userName, Password);
            Assert.AreEqual(termsLink, loginPage.TermWebsite());
        }



    }
}
