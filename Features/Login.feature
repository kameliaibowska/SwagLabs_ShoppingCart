Feature: Login

Scenario: Successful Login with Valid Credentials
    Given I am on the login page
    When I enter username "standard_user" and password "secret_sauce"
    Then I should be taken to Products page