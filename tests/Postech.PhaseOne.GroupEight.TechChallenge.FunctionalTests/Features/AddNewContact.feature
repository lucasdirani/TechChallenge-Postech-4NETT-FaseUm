Feature: Add a new contact

Contact administrators can register a new contact.

@standardscenario
Scenario: Add a new contact
	Given that the name of the new contact is "José Paulo" "Bezerra Maciel Júnior"
	And the email address of the new contact is "paulinho8@gmail.com"
	And the phone number of the new contact is "(11)985640917"
	And the new contact has not yet been registered by the contact administrator
	When the contact administrator registers the new contact
	Then the new contact must be registered successfully