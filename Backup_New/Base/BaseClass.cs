using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Backup.Helpers;
using Backup.Reports;
using Microsoft.Win32;
//using Backup_New;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Help = Backup.Helpers.Help;

namespace Backup.Base
{
    
    class  BaseClass 
    {


        public static IWebDriver driver;


        public string userName = "setup5.4.2";
        public string Password = "1234";
        public string farmCode = "1076002902";

        //Links QA\Alpha\GA
        #region
        private string Alpha = "https://afimilkbackupwebalpha.z6.web.core.windows.net/#/home";
        private string QA = "https://afimilkbackupwebqa.z6.web.core.windows.net/#/login";
        private string GA = "https://afimilkbackupweb.z6.web.core.windows.net/#/login";
        private string dev = "https://afimilkbackupwebdev.z6.web.core.windows.net/#/login";
        #endregion
        public Reporter reporter = new Reporter();
        
        public string reportPath = @"C:\Webapps Report\Backup";
        public static string version;


        
        //לפני כל הטסטים

        [OneTimeSetUp]
        public void Setup()
        {
            
            Help help = new Help();

            if (Reporter.extent == null)
            {


                Rectangle resolution = Screen.PrimaryScreen.Bounds;

                driver = new ChromeDriver();


                string apiVersion = getApiVersion();
                string FunctionVersion = getFunctionVersion();
                Reporter.extent = new ExtentReports();
                Reporter.extent.AddSystemInfo("Browser", "Chrome");
                Reporter.extent.AddSystemInfo("Browser Version ", "dsa");
                Reporter.extent.AddSystemInfo("Resolution", resolution.ToString());
                Reporter.extent.AddSystemInfo("Function version", FunctionVersion.Replace("\"",""));
                Reporter.extent.AddSystemInfo("Api Version", apiVersion);
                driver.Navigate().GoToUrl(QA);

                //get version
                version = help.currentVersion(driver);
                Reporter.extent.AddSystemInfo("Build Version", version.Split(' ')[2]);
                //set path
                reporter.setPath(reportPath, version);

                driver.Close();
            }
            
        }

        private string getApiVersion()
        {
            driver.Navigate().GoToUrl("https://afimilkcockpit-qa.azurewebsites.net/version");
            string version = driver.FindElement(By.CssSelector("#folder0 > div.opened > div:nth-child(2) > span:nth-child(2)")).Text;
            return version;
        }

        private string getFunctionVersion()
        {
            driver.Navigate().GoToUrl("https://afimilkcockpitfunctionappqa.azurewebsites.net/api/version?code=ejK8mAWwatCS3TaNoHAORwq652dRIJfxFnaX8g5n1Ev4gYsr6tT1gg==");
            string version = driver.FindElement(By.CssSelector("body > pre")).Text.Split(',')[0].Split(':')[1];
            return version;


        }

        //אחרי כל הטסטים
        [OneTimeTearDown]
        public void thisIsTheEnd()
        {
            Reporter.extent.Flush();

        }


        //לפני כל טסט
        [SetUp]
        public void openAndRun()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(QA);
            driver.Manage().Window.Maximize();
            
            reporter.startReporting();
        }


        //אחרי כל טסט
        [TearDown]
        public void close()
        {
            reporter.testReporting(driver);
            driver.Quit();
        }


    }
}
