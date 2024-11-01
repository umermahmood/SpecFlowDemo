Feature: Plan a Journey

  Scenario: Plan a valid journey from Leicester Square to Covent Garden
    Given the user is on the journey planner page 
    And the user accepts all cookies if prompted
    When the user plans a journey from "Leicester Square U" to "Covent Garden U"
    Then the journey time for walking and cycling should be displayed

  Scenario: Edit preferences to select least walking route
    Given the user is on the journey planner page
    And the user accepts all cookies if prompted
    When the user plans a journey from "Leicester Square U" to "Covent Garden U"
    And the user selects "least walking" in preferences
    Then the journey time should be '11' minutes

  Scenario: View details for a planned journey
    Given the user is on the journey planner page
    And the user accepts all cookies if prompted
    When the user plans a journey from "Leicester Square U" to "Covent Garden U"
    And the user selects "least walking" in preferences
    And the user clicks on "View Details"
    Then complete access information for "Covent Garden Underground Station" should be displayed

  Scenario: Attempt to plan a journey with invalid locations
    Given the user is on the journey planner page
    And the user accepts all cookies if prompted
    When the user plans a journey from "Leicester Square U" to "Invalid Location +-"
    Then the widget should not provide any journey results

  Scenario: Attempt to plan a journey with no locations entered
    Given the user is on the journey planner page
    And the user accepts all cookies if prompted
    When the user attempts to plan a journey without entering locations
    Then the widget should display an error or prevent the journey planning