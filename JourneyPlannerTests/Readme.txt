
Journey Planner Tests - README
Overview
This project consists of automated tests for a Journey Planner application using Selenium WebDriver and MSTest framework. The tests cover functionalities related to journey planning, including entering locations, validating journey times, and handling cookies.

Development Decisions
1. Page Object Model (POM) Pattern
The project follows the Page Object Model design pattern, which promotes the separation of test logic and UI element management. This makes the code more maintainable and scalable.
Each page class represents a specific page of the application, encapsulating its elements and actions.

2. Use of Explicit Waits
To handle dynamic content loading and improve test reliability, explicit waits (using WebDriverWait) are used instead of Thread.Sleep.
This ensures that the tests wait only as long as necessary for elements to become clickable or visible, improving test performance and reliability.

3. Error Handling
Basic error handling is implemented using try-catch blocks to gracefully manage scenarios where elements may not be found or actions may not complete as expected. This helps in avoiding test failures due to transient UI issues.

4. Element Locators
Element locators are defined using CSS selectors and XPath expressions. This provides flexibility and robustness in locating UI elements. Care was taken to use unique identifiers to avoid ambiguity.
Private properties are used to encapsulate the element locators, promoting reusability and encapsulation.

5. Assertions
Assertions are used extensively to validate expected outcomes in tests. This ensures that the tests fail when the actual behavior does not match the expected behavior, providing clear feedback during test execution.
Specific assertions for error messages and validation help confirm the correctness of the application’s response to user input.

6. Console Logging
Console messages are added to provide runtime feedback about the progress and outcomes of tests. This helps in debugging and understanding the flow of the test execution.

7. Test Organization
The tests are organized in a structured manner within the JourneyPlannerTests.Pages namespace, making it easier to navigate through different page objects and their associated actions.
Getting Started
To run the tests, ensure that you have the following prerequisites:

.NET SDK installed.
Necessary NuGet packages (Selenium.WebDriver, Selenium.Support, MSTest.TestFramework).
A compatible browser driver (e.g., ChromeDriver for Chrome) is available in your system's PATH.

Running Tests
Open the solution in Visual Studio.
Build the project to restore NuGet packages.
Run the tests using the Test Explorer or by executing the tests through the command line.

Conclusion
This test automation framework aims to provide a robust and maintainable solution for testing the Journey Planner application. Continuous improvements will be made to enhance coverage and handle additional features as the application evolves.

