using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace FindAZipp.Pages.NavigationPage
{
    public partial class NavigationPage
    {
        public IWebElement citiesWithI
        {
            get
            {
                return this.Driver.FindElement(By.XPath(
                    "/html/body/table[2]/tbody/tr/td[3]/table/tbody/tr/td/table[2]/tbody/tr/td[1]/table[5]/tbody/tr/td/a[9]"));
            }
        }
    }
}
