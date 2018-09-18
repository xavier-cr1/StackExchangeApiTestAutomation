Feature: How to Manage an Answer API
	In order to test answers apis
	As a QA
	I want to be sure that all of them work

@mytag
Scenario: Retrive answer with Id, by order, sort and site and compare with expected
	Given I want to Get a answer with the id '52391507' with order 'desc' sorted by 'activity' and site 'stackoverflow'
	Then The answer with the id '52391507' is equal to expected
