
# CapgeminiElections

A web app thats purpose is to help users stay aware of local and federal elections. Provides information on local and federal elections, information on local candidates, and local political news. A more detailed layout of the project can be found on the [Detail](https://github.com/SCCapstone/CapgeminiElections/wiki/Design) and [Architecture](https://github.com/SCCapstone/CapgeminiElections/wiki/Architecture) wiki pages.


## Technologies
In order to build this project you will need to install:

* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019)

* [.NET Core 3.0 SDK or later](https://dotnet.microsoft.com/download/dotnet-core/3.0)

* [Azure Cosmos DB](https://azure.microsoft.com/en-us/try/cosmosdb/)

You have to install Visual studio and .Net Core using their installers.

## Code style
This project uses [Googles style guide.](https://google.github.io/styleguide/)

## Testing App

 Phone Notifications: Must verify phone # in Twilio because this is a trial account. Go to 
    1. https://www.twilio.com/console
          Log in with info given to you. 
    2. click on the 3-dots. 
    3. go to the phone #
    4. go to verified caller id's
    5. click on the red +
    6. verify #.
    
 Now you can get texts from us. 
 ### Behaviorial tests
In order to perform the behaviorial test the folder named BDDTests needs to be moved into your repos folder for visual studio. Once it's moved you should see BDDTests located under your solutions explorer, if it's not you will need to right click on "Solutions 'CapgeminiSolutions'"  click add>Existing project and select the BDDTests folder. Once BDDTests has been added build the project, tests should now appear in the test explorer and should run as long as web app is deployed.

## Authors

Name | Email | 
-----|-------|
Mason Turner|maturner@email.sc.edu|
Christian Meetze|ccmeetze@email.sc.edu|
Ashton Bass|anbass@email.sc.edu|
Taylor Gordon|tag2@email.sc.edu|

