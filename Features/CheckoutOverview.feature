Feature: CheckoutOverview

Scenario: Checkout overview contents are correct
	Given I login to examine checkout overview page
	When I add product and checkout it
	Then Checkout overview page is loaded with correct content
