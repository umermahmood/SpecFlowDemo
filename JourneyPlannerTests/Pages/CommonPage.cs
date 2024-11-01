using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyPlannerTests.Pages
{
    public class CommonPage
    {
        public static IWebDriver driver;
        public static class Timings
        {
            public const int SECONDS_1 = 1;
            public const int SECONDS_2 = 2;
            public const int SECONDS_3 = 3;
            public const int SECONDS_5 = 5;
            
        }

        //public void WaitForLoader(int secondsCount, int maxWaitCount, By Locator)
        //{
        //    try
        //    {
        //        WebDriverWait waitGIF = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsCount));
        //        waitGIF.IgnoreExceptionTypes(typeof(NoSuchElementException));
                

        //        WebDriverWait waitGIFVisible = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitCount));
        //        waitGIFVisible.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(Locator));
        //    }
        //    catch (WebDriverTimeoutException)
        //    {
        //        //   No spinner handled (does not always appear).
        //        return;
        //    }
        //}

        public void SetUpWebDriverWait(int secondsCount, By controlName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsCount));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.IgnoreExceptionTypes(typeof(WebDriverException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(controlName));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(controlName));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(controlName));
            wait.Until(x => x.FindElement(controlName));
        }
    }
}
