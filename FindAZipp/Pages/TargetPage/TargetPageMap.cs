using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FindAZipp.Pages.TargetPage
{
    public partial class TargetPage
    {
        public List<IWebElement> Cities
        {
            get
            {
                IWebElement reminder =
                    this.Driver.FindElement(By.XPath(
                        "/html/body/table[2]/tbody/tr/td[3]/table[1]/tbody/tr/td/table[2]/tbody/tr/td[3]/table[2]/tbody/tr[4]/td"));

                List<IWebElement> list = reminder.FindElements(By.TagName("a")).ToList();

                return list;
                
            }
        }
    }
}
