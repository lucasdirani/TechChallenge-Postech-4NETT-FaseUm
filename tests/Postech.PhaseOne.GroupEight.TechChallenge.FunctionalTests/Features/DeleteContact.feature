Feature: Delete a contact

Contact administrators can delete any registered contact.

@standardscenario
Scenario: Delete a registered contact
	Given that the name of the registered contact is "Alexandre" "Silva"
	And the email address of the registered contact is "alexandresilva@gmail.com"
	And the phone number of the registered contact is "(11)985451360"
	And the registered contact is active in the contact list
	When the contact administrator chooses to delete this registered contact
	Then the registered contact must be deleted