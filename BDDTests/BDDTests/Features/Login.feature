Feature: Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Successful Login by User
	Given I Navigate to the Login page
	When I Login with Username 'user' and Password 'password' on the Login Page
	Then logout button should be visable on home page
