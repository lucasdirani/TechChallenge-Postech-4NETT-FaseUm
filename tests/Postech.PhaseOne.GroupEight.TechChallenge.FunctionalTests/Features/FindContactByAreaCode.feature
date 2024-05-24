Feature: Find contact by area code

Contact administrators can consult registered contacts by telephone area code

@standardscenario
Scenario: Find contact by area code
	Given that the contact administrator wants to consult all contacts registered with area code "11"
	And the contact administrator has 10 active contacts registered with area code "11"
	When the contact administrator searches contacts by area code
	Then the contact administrator must view the data of the contacts registered with the selected area code