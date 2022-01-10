using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Threading;
using NUnit.Framework.Internal;

namespace Backup.Reports
{
    class Reporter
    {

        public static ExtentReports extent;
        public ExtentTest test;
        private IWebDriver driver;
        public static int scrennshotNumber = 0;
        public string Fullscreenshotpath;


        public static string version;

        private static string screenshotpath;
        public string disc;



        public void setPath(string path, string version)
        {
            var spark = new ExtentSparkReporter(path+ @"\"+version + @"\Report.html");
            screenshotpath = path + @"\" + version +@"\Screenshots\" ;
            extent.AttachReporter(spark);

        }

        internal void startReporting()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Properties.Get("Description").ToString());
            disc = TestContext.CurrentContext.Test.Properties.Get("Description").ToString();
            
        }

        internal void testReporting(IWebDriver driver)
        {
            this.driver = driver;
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            Status logstatus;
            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    test.Log(logstatus, "Test ended with error " + test.AddScreenCaptureFromPath(screenShot(disc)));
                    test.Log(logstatus, TestContext.CurrentContext.Result.Message.ToString());
                    
                    
                    break;
                default:
                    logstatus = Status.Pass;
                    test.Log(logstatus, "Pass" + test.AddScreenCaptureFromPath(screenShot(disc)));
                    break;
            }
        }

        public string screenShot(string testDisc)
        {

            scrennshotNumber++;
            Fullscreenshotpath = screenshotpath + scrennshotNumber.ToString() + ".png";

            if (!Directory.Exists(screenshotpath))
            {
                DirectoryInfo di = Directory.CreateDirectory(screenshotpath);
            }

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Fullscreenshotpath, ScreenshotImageFormat.Png);
            return Fullscreenshotpath;
        }
        public void logger(string info, IWebDriver driver)
        {
            this.driver = driver;
            Thread.Sleep(220);
            test.Info(info, MediaEntityBuilder.CreateScreenCaptureFromPath(screenShot(disc)).Build());
        }
    }
}
