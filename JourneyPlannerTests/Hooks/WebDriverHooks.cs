using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace JourneyPlannerTests.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public WebDriverHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetupWebDriver()
        {
            // Create ChromeOptions and configure it to start maximized
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");

            // Pass options to the ChromeDriver
            var driver = new ChromeDriver(options);
            _scenarioContext.Set<IWebDriver>(driver);
        }

        [AfterScenario]
        public void TearDownWebDriver()
        {
            // Retrieve and dispose the WebDriver instance after each scenario
            if (_scenarioContext.TryGetValue(out IWebDriver driver))
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
