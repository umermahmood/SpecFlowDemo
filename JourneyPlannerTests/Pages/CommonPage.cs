using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace JourneyPlannerTests.Pages
{
    public class CommonPage
    {
        protected IWebDriver driver;
        protected WebDriverWait _wait;

        public CommonPage(IWebDriver driver, int waitTimeInSeconds = 10)
        {
            this.driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeInSeconds));
        }

        public void WaitAndClick(IWebElement element)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
        }

        public void WaitUntilVisible(By locator)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        // Helper method to scroll to an element and click it using JavaScript
        public void ScrollAndClick(IWebElement element)
        {
            try
            {
                // Attempt to scroll into view and wait for the element to be clickable
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'});", element);
                _wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
            }
            catch (ElementClickInterceptedException)
            {
                // Retry by scrolling a bit more and clicking again if an overlay intercepts the click
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, -100);"); // Adjust the offset as needed
                _wait.Until(ExpectedConditions.ElementToBeClickable(element)).Click();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Failed to scroll and click the element within the specified time.");
            }
        }
    }
}
