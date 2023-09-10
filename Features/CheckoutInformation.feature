Feature: CheckoutInformation

Scenario: Checkout information contents are correct
	Given I login to examine checkout information page
	When I add product in shopping cart and press checkout
	Then I fill "FName", "LName" and "ZipCode" in contacts to examine result

Scenario: Checkout information contents with empty first name
	Given I login to examine checkout information page
	When I add product in shopping cart and press checkout
	Then I fill "", "LName" and "ZipCode" in contacts to examine result

Scenario: Checkout information contents with empty last name
	Given I login to examine checkout information page
	When I add product in shopping cart and press checkout
	Then I fill "FName", "" and "ZipCode" in contacts to examine result

Scenario: Checkout information contents with empty zip code
	Given I login to examine checkout information page
	When I add product in shopping cart and press checkout
	Then I fill "FName", "LName" and "" in contacts to examine result