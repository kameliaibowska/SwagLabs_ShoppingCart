Feature: ProductDetails

Scenario: Product details contents are correct
	Given I login to examine product details page
	When I select product in products page
	Then Product details page is loaded with correct content
