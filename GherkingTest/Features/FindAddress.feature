

Feature: FindAddress

Simple calculator for adding two numbers
Background: 
	Given im an anonymous user

@mytag
Scenario: I want to find an address
	Given I Have the address "H.C. Andersen Haven 1, 5000 Odense C"
	And I Have a fresh webpage open
	When I input the address into the "search bar"
	And I click "Search"
	Then the result should be displayed

Scenario: I want to find an address but i got it wrong postal number
	Given I have the address "H.C. Andersen Haven 1, 1000 Odense C"
	And I Have a fresh webpage open
	When I input the address into the "search bar"
	And I click "Search"
	Then an error should be displayed showing where i made a mistake

Scenario: I want to find an address but i do not have the postal number
	Given I have the address "H.C. Andersen Haven 1, Odense"
	And I Have a fresh webpage open
	When I input the address into the "search bar"
	And I click "Search"
	Then the result should be displayed

Scenario: I want to find an address but i dont know the city and the address exists more than once
	Given I have the address "Christiansgade 42"
	And I Have a fresh webpage open
	When I input the address into the "search bar"
	And I click "Search"
	Then the result should be displayed 
	And a popup should alert me that i go more than one result

Scenario: I want to find an address but i did not speisfy unit number
	Given I have the address "Gartnerbyen 72, 5200 odense v"
	And I Have a fresh webpage open
	When I input the address into the "search bar"
	And I click "Search"
	Then the result should be displayed for the entire building


	
