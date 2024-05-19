

Feature: FindAddressess

Simple calculator for adding two numbers
Background: 
	Given im an anonymous user

@mytag
Scenario: I want to find 2 addresses
	Given I Have the address "H.C. Andersen Haven 1, 5000 Odense C"
	And I Have the second adress "Kongensgade 1, 5000 Odense C"
	And I Have a fresh webpage open
	When I input the 1st address into the "search bar 1"
	And I input the 2nd adress into the "search bar 2"
	And I click "Search"
	Then the result should be displayed for the "1" address
	And the result should be displayed for the "2" address

Scenario: I want to find an address and another address where i got it wrong postal number
	Given I have the address "H.C. Andersen Haven 1, 1000 Odense C"
	And I Have the second adress "Kongensgade 1, 5000 Odense C"
	And I Have a fresh webpage open
	When I input the 1st address into the "search bar 1"
	And I input the 2nd adress into the "search bar 2"
	And I click "Search"
	Then an error should be displayed showing where i made a mistake
	And And the result should be displayed for the "2" address

Scenario: I want to find 2 addresses but neither of them exist
	Given I have the address "Christiansgade 42, 10000000 Købmanshavn"
	And I Have the second adress "Kongensgade 1, 12000000 Oderse k"
	And I Have a fresh webpage open
	When I input the 1st address into the "search bar 1"
	And I input the 2nd adress into the "search bar 2"
	And I click "Search"
	Then an error should be displayed showing where i made a mistake

Scenario: I want to find 2 addresses but i dont know the city of one of them and the address exists more than once
	Given I have the address "Christiansgade 42"
	And I Have the second adress "Kongensgade 1, 5000 Odense C"
	And I Have a fresh webpage open
	When I input the 1st address into the "search bar 1"
	And I input the 2nd adress into the "search bar 2"
	And I click "Search"
	Then the result should be displayed with prefrence for having the addresses being in the same city
	And a popup should alert me that i got more than one result

Scenario: I want to find an address but i only speisfy unit number one one
	Given I have the address "Gartnerbyen 72, 5200 odense v"
	And I have the address "Gartnerbyen 72, 1.3, 5200 odense v"
	And I Have a fresh webpage open
	When I input the 1st address into the "search bar 1"
	And I input the 2nd adress into the "search bar 2"
	And I click "Search"
	Then the result should be displayed for the "entire building" adress
	And the result should be displayed for the "one unit" adress


	
