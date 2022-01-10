using Backup.Reports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
//using Squirrel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Backup.Elements
{
    class frontPage_POM
    {

        private IWebDriver driver;
        public frontPage_POM(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "sh-download-icon")]
        private IList<IWebElement> backupsDwonload { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "#pauseOrResume")]
        public IWebElement downloadElabled { get; set; }
                        
        [FindsBy(How = How.Id, Using = "settingsBtn")]
        public IWebElement userSettings { get; set; }
                                        
        [FindsBy(How = How.Id, Using = "dropdownLanguage")]
        public IWebElement langsDropdown { get; set; }

                                                        
        [FindsBy(How = How.ClassName, Using = "confirm")]
        public IWebElement apply { get; set; }

                
        [FindsBy(How = How.ClassName, Using = "sh-date")]
        public IList<IWebElement> dates { get; set; }
                
        [FindsBy(How = How.ClassName, Using = "dropdown-item")]
        public IList<IWebElement> langs { get; set; }




        public int numberOfBackups()
        {
            return backupsDwonload.Count();

        }

        public void downloadLastFile()
        {
            backupsDwonload[1].Click();
            Thread.Sleep(1000);
            driver.FindElement(By.ClassName("btn-p2")).Click();
            Thread.Sleep(1000);


        }

        public bool FileDownloaded(int min)
        {

            var directory = new DirectoryInfo(@"C:\Users\ofir_s\Downloads");
            var myFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();
            int flag = 0;
            string x = myFile.ToString();
            while (!x.Last().ToString().Equals("p"))
            {
                if (flag > min)
                {
                    return false;
                }
                flag++;
                Thread.Sleep(60 * 1000);
                x = (from f in directory.GetFiles()
                     orderby f.LastWriteTime descending
                     select f).First().ToString(); ;
            }
            return true;

           

        }

        public bool checkDates()
        {
            DateTime first = new DateTime();
            DateTime second = new DateTime();
            int counter = 0;
            for (int i = 0; i < dates.Count()-1; i++)
            {

                string x = dates[i].Text;
                string y = dates[i+1].Text;
                try { first = DateTime.ParseExact(x, "MM/dd/yy", CultureInfo.InvariantCulture); } catch { first = DateTime.ParseExact(x, "MM/d/yy", CultureInfo.InvariantCulture); }
                try { second = DateTime.ParseExact(y, "MM/dd/yy", CultureInfo.InvariantCulture); } catch { second = DateTime.ParseExact(y, "MM/d/yy", CultureInfo.InvariantCulture); }
                var diff2 = (second - first).TotalDays;
                //int diff2 = Int32.Parse((second - first).ToString());
                if (i <= 2)
                {
                    if (diff2 != 7)
                    {
                        if (i == 2)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (diff2 != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool runLangs(Reporter reporter)
        {
            IList<IWebElement> times = driver.FindElements(By.ClassName("sh-time"));
            IList<IWebElement> sizes = driver.FindElements(By.ClassName("sh-file-size"));
            List<string> timesA = getTexts(times);
            List<string> sizesA = getTexts(sizes);

            for (int i = 0; i < 26; i++)
            {

                Thread.Sleep(2000);
                userSettings.Click();
                Thread.Sleep(1000);
                langsDropdown.Click();
                Thread.Sleep(1000);
                langs[i].Click();
                apply.Click();
                reporter.logger(langs[i].Text,driver);
                Thread.Sleep(2000);
                if (!copareLists(timesA, sizesA))
                {
                    return false;
                
                };
                Thread.Sleep(2000);
            }
            return true;


        }

        private bool copareLists(List<string> timesA, List<string> sizesA)
        {
            IList<IWebElement> times = driver.FindElements(By.ClassName("sh-time"));
            IList<IWebElement> sizes = driver.FindElements(By.ClassName("sh-file-size"));
            List<string> timesB = getTexts(times);
            List<string> sizesB = getTexts(sizes);

            if (timesA.SequenceEqual(timesB) && sizesA.SequenceEqual(sizesB))
            {
                return true;
            }
            return false;

        }

        private List<string> getTexts(IList<IWebElement> listA)
        {
          
            List<string> ans = new List<string>();
            foreach (var item in listA)
            {
                ans.Add(item.Text);
            }
            return ans;

        }

    }
}
