Feature: Update a contact

Contact administrators can update any registered contact.

@standardscenario
Scenario: Update a registered contact
	Given that the provided name of the registered contact is "José Paulo" "Bezerra Maciel Júnior"
	And the provided email address of the registered contact is "paulinho8@gmail.com"
	And the provided phone number of the registered contact is "(11)985640917"
	And the registered contact is current active 
	And the contact administrator changes the first name of the registered contact to "Paulinho"
	And the contact administrator changes the last name of the registered contact to "Bezerra"
	And the contact administrator changes the email address of the registered contact to "josepaulobezerra@gmail.com"
	And the contact administrator changes the phone number of the registered contact to "(31)995750929"
	And the contact with the updated data has not yet been registered by the contact administrator
	When the contact administrator confirms the change in registered contact data
	Then the registered contact data must be updated