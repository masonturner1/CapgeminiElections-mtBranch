Feature: NavigateToLogin
	In order to login
	As a user
	I want to go to the login page

@mytag
Scenario: Navigate to Login
	Given Im a user on the homepage
	When I press login in the upper corner
	Then I should navigate to the login page