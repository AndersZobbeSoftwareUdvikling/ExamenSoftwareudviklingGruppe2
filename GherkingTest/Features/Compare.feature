

Feature: Compare

Simple calculator for adding two numbers
Background: 
	Given im an anonymus user
	And i have found the address "H.C. Andersen Haven 1, 5000 Odense C"
	And i have found the address "Kongensgade 1, 5000 Odense C"

@mytag
Scenario: I want to compare adresses
	When I click "Compare"
	Then the result should be displayed

	
