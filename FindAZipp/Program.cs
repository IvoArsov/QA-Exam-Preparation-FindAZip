using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FindAZipp.Models;
using FindAZipp.Pages.ContentPage;
using FindAZipp.Pages.GooglePage;
using FindAZipp.Pages.NavigationPage;
using FindAZipp.Pages.TargetPage;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FindAZipp
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            //FirefoxProfile profile = new FirefoxProfile();
            //profile.AddExtension(@"..\..\Extensions\adblock_firefox.xpi");
            //FirefoxDriver driver = new FirefoxDriver(profile);

            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"..\..\Extensions\adblock_chrome.crx");
            ChromeDriver driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            driver.Url = @"http://findazip.com/";

            NavigationPage nav = new NavigationPage(driver);
            nav.citiesWithI.Click();

            TargetPage target = new TargetPage(driver);

            var cities = target.Cities;

            ContentPage content = new ContentPage(driver);
            var citiesURLs = new List<string>();

            for (int i = 0; i < 10; i++)
            {
               citiesURLs.Add(cities[i].GetAttribute("href"));
            }


            var citiesForClick = new List<CityInfo>();
            foreach (var cityUrL in citiesURLs)
            {
                driver.Url = cityUrL;

                citiesForClick.Add(new CityInfo(content.Name.Text,
                                                content.State.Text,
                                                content.Zip.Text,
                                                content.Longitude.Text,
                                                content.Latitude.Text));
            }

            driver.Url = @"https://www.google.bg/maps/";

            GooglePage google = new GooglePage(driver);

            foreach (var city in citiesForClick)
            {
                google.SearchField.Clear();
                google.SearchField.SendKeys($"{city.Latitude}, {city.Longitude}");
                google.SearchField.SendKeys(Keys.Enter);

                //driver.Url = $"https://www.google.bg/maps/@{city.Latitude},{city.Longitude},10z";
               

                Thread.Sleep(2000);
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile($"{city.Name}-{city.State}-{city.Zip}.jpg", ScreenshotImageFormat.Jpeg);
            }
        }
    }
}
