Feature: ShoppingCart

Scenario: Checkout overview contents are correct
	Given I login to examine shopping cart page
	When I add product in shopping cart and navigate to it
	Then Shopping cart page is loaded with correct content
