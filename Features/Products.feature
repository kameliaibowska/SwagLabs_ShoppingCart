Feature: Products

Scenario: Order products by price low to high
	Given I login to examine products page
	When I select on "price low to high" option in Products page
	Then Products are ordered by price "low to high"

Scenario: Order products by price high to low
	Given I login to examine products page
	When I select on "price high to low" option in Products page
	Then Products are ordered by price "high to low"

Scenario: Order products by name a to z
	Given I login to examine products page
	When I select on "name a to z" option in Products page
	Then Products are ordered by name "a to z"

Scenario: Order products by name z to a
	Given I login to examine products page
	When I select on "name z to a" option in Products page
	Then Products are ordered by name "z to a"