using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backup.Base;
using Backup.Elements;
using Backup.Helpers;
using NUnit.Framework;

namespace Backup_New.Tests
{
    internal class licensing : BaseClass
    {
        loginPage_POM Licens;

        [SetUp]
        public void setup()
        {
            Licens = new loginPage_POM(driver);
        }


        [Test]
        [Description("Full - active user")]
        public void test1()
        {
            Help help = new Help();
            help.changeLicensing(userName, Password, farmCode, "13/05/2024", "Full");
            bool ans = Licens.assertLogin(userName, Password);
            Assert.IsTrue(ans);
        }

    }
}
