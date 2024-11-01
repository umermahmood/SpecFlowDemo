using JourneyPlannerTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;

namespace JourneyPlannerTests.Steps
{
    [Binding]
    public class JourneyPlannerSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly JourneyPlannerPage _journeyPlannerPage;

        public JourneyPlannerSteps(ScenarioContext scenarioContext)
        {
            _driver = scenarioContext.Get<IWebDriver>();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _journeyPlannerPage = new JourneyPlannerPage(_driver);
        }

        [Given(@"the user accepts all cookies if prompted")]
        public void GivenTheUserAcceptsAllCookiesIfPrompted()
        {
            _journeyPlannerPage.AcceptCookies();
        }

        [Given(@"the user is on the journey planner page")]
        public void GivenTheUserIsOnTheJourneyPlannerPage()
        {
            _driver.Navigate().GoToUrl("https://tfl.gov.uk/plan-a-journey/");

        }

        [When(@"the user plans a journey from ""(.*)"" to ""(.*)""")]
        public void WhenTheUserPlansAJourney(string from, string to)
        {
            _journeyPlannerPage.EnterFromLocation(from);
            _journeyPlannerPage.EnterToLocation(to);
            _journeyPlannerPage.PlanJourney();
        }

        [Then(@"the journey time for walking and cycling should be displayed")]
        public void ThenTheJourneyTimeForWalkingAndCyclingShouldBeDisplayed()
        {
            _journeyPlannerPage.JourneyTime();
            
        }

        [When(@"the user selects ""(.*)"" in preferences")]
        public void WhenTheUserSelectsInPreferences(string preference)
        {
                _journeyPlannerPage.SelectEditPreferences();
        }

        [Then(@"the journey time should be '([^']*)' minutes")]
        public void ThenTheJourneyTimeShouldBeMinutes(string expectedTime)
        {
            _journeyPlannerPage.JourneyTimeValidation();
        }

        
        [When(@"the user clicks on ""(.*)""")]
        public void WhenTheUserClicksOn(string button)
        {
             _journeyPlannerPage.ClickViewDetails();
        
        }

        [Then(@"complete access information for ""(.*)"" should be displayed")]
        public void ThenCompleteAccessInformationShouldBeDisplayed(string station)
        {
            _journeyPlannerPage.GetAccessInformation();
        }

        [When(@"the user attempts to plan a journey without entering locations")]
        public void WhenTheUserAttemptsToPlanAJourneyWithoutEnteringLocations()
        {
            _journeyPlannerPage.PlanJourney();
        }

        [Then(@"the widget should display an error or prevent the journey planning")]
        public void ThenTheWidgetShouldDisplayAnError()
        {
             _journeyPlannerPage.GetErrorMessage();
           
        }

        [Then(@"the widget should not provide any journey results")]
        public void ThenTheWidgetShouldNotProvideAnyJourneyResults()
        {
            _journeyPlannerPage.GetJourneyResult();
        }
    }

}
