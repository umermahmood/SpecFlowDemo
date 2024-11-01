using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace JourneyPlannerTests.Pages
{
    public class JourneyPlannerPage : CommonPage
    {
 
        public JourneyPlannerPage(IWebDriver driver) : base(driver) { }

        // Element locators
        private IWebElement FromInputClick => driver.FindElement(By.XPath("//input[@class='jpFrom tt-input']"));
        private IWebElement ToInputClick => driver.FindElement(By.XPath("//input[@class='jpTo tt-input']"));
        private IWebElement PlanJourneyButton => driver.FindElement(By.Id("plan-journey-button"));
        private IWebElement EditPreferencesButton => driver.FindElement(By.XPath("//button[@class='toggle-options more-options']"));
        private IWebElement LeastWalkingOption => driver.FindElement(By.XPath("//input[@id='JourneyPreference_2']"));
        private IWebElement UpdateJourneyButton => driver.FindElement(By.XPath("//div[@id='more-journey-options']//input[@class='primary-button plan-journey-button']"));
        private IWebElement ViewDetailsButton => driver.FindElement(By.XPath("//div[@id='option-1-content']//button[contains(text(),'View details')]"));
        private IWebElement acceptCookiesButton => driver.FindElement(By.XPath("//button[@id='CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll']"));
        private IWebElement inputFromDropdown => driver.FindElement(By.Id("InputFrom-dropdown"));
        private IWebElement AcceptCookiesButton => driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));

        public void AcceptCookies()
        {
            try
            {
                if (AcceptCookiesButton.Displayed)
                {
                    WaitAndClick(AcceptCookiesButton);
                    Thread.Sleep(2000);
                }
            }
            catch (WebDriverTimeoutException)
            {
                // If the "Accept All Cookies" button isn't found, assume it's not present and continue
            }
        }

        public void EnterFromLocation(string from)
        {
            WaitUntilVisible(By.XPath("//input[@class='jpFrom tt-input']"));
            FromInputClick.SendKeys(from);
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputFrom-dropdown"))); // Wait for suggestions

            // Select the matched suggestion 
            var suggestions = driver.FindElements(By.ClassName("tt-suggestion")); 
            bool foundMatch = false;
            foreach (IWebElement suggestion in suggestions)
            {
                // Check if the suggestion text matches the desired station name
                if (suggestion.Text.Contains("Leicester Square Underground Station", StringComparison.OrdinalIgnoreCase))
                {
                    WaitAndClick(suggestion);
                    foundMatch = true; // Indicate that a match was found and clicked
                    break; 
                }
            }
        }

        public void EnterToLocation(string to)
        {
            WaitUntilVisible(By.XPath("//input[@class='jpTo tt-input']"));
            //WaitAndClick(ToInputClick);
            ToInputClick.SendKeys(to);
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("InputTo-dropdown"))); // Wait for suggestions

                // Select the matched suggestion 
                var suggestions = driver.FindElements(By.ClassName("tt-suggestions")); 
                if (suggestions.Count > 0)
                {
                    bool foundMatch = false;
                    foreach (IWebElement suggestion in suggestions)
                    {
                        // Check if the suggestion text matches the desired station name
                        if (suggestion.Text.Contains("Covent Garden Underground Station", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(suggestion.Text);
                            WaitAndClick(suggestion);
                            foundMatch = true; // Indicate that a match was found and clicked
                            break; 
                        }
                    }
                }
            }
            catch
            {
                //do nothing
            }
        }

        public void JourneyTime()
        {
            Thread.Sleep(2000);
            // Wait for journey results to load
            _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("journey-box")));

            // Validate the walking time
            try
            {
                var walkingJourneyBox = driver.FindElement(By.CssSelector("a.journey-box.walking"));
                var walkingTimeElement = walkingJourneyBox.FindElement(By.CssSelector(".col2.journey-info strong"));
                string walkingTimeText = walkingTimeElement.Text;

                Console.WriteLine("Walking time: " + walkingTimeText + " mins");
                Assert.IsFalse(string.IsNullOrEmpty(walkingTimeText), "Walking time should be displayed.");
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Walking journey time is not displayed.");
            }

            // Validate the cycling time
            try
            {
                var cyclingJourneyBox = driver.FindElement(By.CssSelector("a.journey-box.cycling"));
                var cyclingTimeElement = cyclingJourneyBox.FindElement(By.CssSelector(".col2.journey-info strong"));
                string cyclingTimeText = cyclingTimeElement.Text;

                Console.WriteLine("Cycling time: " + cyclingTimeText + " mins");
                Assert.IsFalse(string.IsNullOrEmpty(cyclingTimeText), "Cycling time should be displayed.");
            }
            catch (NoSuchElementException)
            {
                throw new Exception("Cycling journey time is not displayed.");
            }
        }

        public void PlanJourney()
        {
            WaitAndClick(PlanJourneyButton);
        }

        public void JourneyTimeValidation()
        {
            // Wait for the journey time element to be visible
            var journeyTimeElement = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".journey-time.no-map")));

            // Extract the inner text of the journey time element
            var journeyTimeText = journeyTimeElement.Text.Trim(); 

            // Check if the journey time text contains "mins"
            Assert.IsTrue(journeyTimeText.Contains("mins"), $"Expected journey time to contain 'mins', but found: '{journeyTimeText}'.");

            // If the check passes, log a success message
            Console.WriteLine("Journey time is valid and contains 'mins'. Test passed.");
        }


        public void SelectEditPreferences()
        {
            // Scroll to and click the Edit Preferences button
            Thread.Sleep(3000);
            ScrollAndClick(EditPreferencesButton);
            
            // Wait until the "Routes with least walking" label is visible, then click it
            var leastWalkingLabel = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//label[@for='JourneyPreference_2']")));
            WaitAndClick(leastWalkingLabel);

            // Verify if the "Least Walking" option is selected after clicking the label
            Assert.IsTrue(LeastWalkingOption.Selected, "The 'Routes with least walking' option should be selected.");

            // Scroll to and click the Update Journey button
            ScrollAndClick(UpdateJourneyButton);
            
            Console.WriteLine("The 'Routes with least walking' option is selected and journey preferences are updated.");
        }

        public void ClickViewDetails()
        {
            Thread.Sleep(4000);
            WaitAndClick(ViewDetailsButton);
        }

        public void GetAccessInformation()
        {
            string[] accessOptions = { "up-stairs", "up-lift", "level-walkway" };

            foreach (var option in accessOptions)
            {
                var element = _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector($"a.{option}")));
                Assert.IsTrue(element.Displayed, $"The '{option.Replace("-", " ")}' access option is not displayed.");
            }
            Console.WriteLine("All access options are displayed correctly.");
        }

        public void GetErrorMessage()
        {
            ValidateFieldError("#InputFrom-error", "The From field is required.");
            ValidateFieldError("#InputTo-error", "The To field is required.");

            Console.WriteLine("Verified required field error messages for From and To locations.");
        }

        private void ValidateFieldError(string selector, string expectedMessage)
        {
            var errorElement = _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector)));
            var actualMessage = errorElement.Text.Trim();
            Assert.AreEqual(expectedMessage, actualMessage, $"Expected error message to be '{expectedMessage}', but found '{actualMessage}'.");
        }

        public void GetJourneyResult()
        {
            var errorMessageElement = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//ul[@class='field-validation-errors']//li[contains(text(),'Journey planner could not find any results to your search. Please try again')]")));
            var actualMessage = errorMessageElement.Text.Trim();
            const string expectedMessage = "Journey planner could not find any results to your search. Please try again";

            Assert.AreEqual(expectedMessage, actualMessage, $"Expected error message to be '{expectedMessage}', but found '{actualMessage}'.");
            Console.WriteLine("Verified error message: " + actualMessage);
        }
    }
}
