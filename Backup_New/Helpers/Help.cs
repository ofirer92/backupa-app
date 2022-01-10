using Backup.Reports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backup.Helpers
{
    class Help
    {
        private IWebDriver driver;
        public string Fullscreenshotpath;

        public void MainloadingWait(IWebDriver driver)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementExists((By.ClassName("sh-download-icon"))));
            Thread.Sleep(500);
        }

        public string currentVersion(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
            wait.Until(c => c.FindElement(By.ClassName("version-number")));
            string version = driver.FindElement(By.ClassName("version-number")).Text;
            return version;
        }

        public void whileDisplayed(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor jse2 = (IJavaScriptExecutor)driver;

          //  jse2.ExecuteScript("return document.querySelector('downloads-manager').shadowRoot.querySelector('downloads-item').shadowRoot.getElementById('cancel').click()");
            jse2.ExecuteScript("document.querySelector('downloads-manager').shadowRoot.querySelector('#downloadsList downloads-item').shadowRoot.querySelector(cr - button[focus - type = 'cancel']).click()");

        }

        public void changeLicensing(string username, string password, string farmCode, string wantedExpiredDate, string fullOrTrail)
        {
            var proc1 = new ProcessStartInfo();
            string anyCommand = " QA " + username + " " + password + " " + farmCode + " AbcOnTheGo " + wantedExpiredDate + " " + fullOrTrail;
            string path = @"C:\Users\ofir_s\Desktop\UpdateApplicationLicense.exe";
            proc1.UseShellExecute = true;

            proc1.WorkingDirectory = @"C:\Windows\System32";

            proc1.FileName = @"C:\Windows\System32\cmd.exe";
            proc1.Verb = "runas";
            proc1.Arguments = "/K " + path + anyCommand;
            proc1.WindowStyle = ProcessWindowStyle.Normal;
            Process cmdP = new Process();
            cmdP.StartInfo = proc1;
            cmdP.Start();
            Thread.Sleep(7500);
            cmdP.Kill();



        }


    }
}
