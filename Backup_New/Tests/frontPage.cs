using Backup.Base;
using Backup.Elements;
using Backup.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Backup.Base
{
    [TestFixture]
    class frontPage : BaseClass
    {
        private loginPage_POM loginPage;
        private frontPage_POM front;
        private Help help;

        [SetUp]
        public void setup()
        {
            loginPage = new loginPage_POM(driver);
            front = new frontPage_POM(driver);
            help = new Help();

            loginPage.setLoginDetails(userName, Password);
            loginPage.acceptTerms();
            loginPage.clickLogin();
            help.MainloadingWait(driver);
        }

        [Test]
        [Description("Number of backups")]
        public void test1()
        {
            Assert.AreEqual(10, front.numberOfBackups());
        }

        [Test]
        [Description("Assert download is finished")]
        public void test2()
        {
            front.downloadLastFile();
            
            //assert the file is downloaded in the X time you set in the inner method
            Assert.IsTrue(front.FileDownloaded(20));

        }

        [Test]
        [Description("Check dates")]
        public void test3()
        {
            Assert.IsTrue(front.checkDates());
        }

        [Test]
        [Description("Run languages")]
        public void test4()
        {
            Assert.IsTrue(front.runLangs(reporter));
        }


    }
}
