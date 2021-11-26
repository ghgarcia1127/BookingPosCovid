# BookingPosCovid

This app was developed using the following tecnologies
 1. Azure Cosmos DB, for debuging purposses you can install **Azure Cosmos DB Emulator** : https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator
 2. Docker Container : https://docs.docker.com/get-docker/
 3. .Net Core 3.1 : https://dotnet.microsoft.com/download/dotnet/3.1

The requirements resolved on this API are the follows:
- API will be maintained by the hotel’s IT department.
- As it’s the very last hotel, the quality of service must be 99.99 to 100% => no downtime
- For the purpose of the test, we assume the hotel has only one room available
- To give a chance to everyone to book the room, the stay can’t be longer than 3 days and can’t be reserved more than 30 days in advance. 
- All reservations start at least the next day of booking,
- To simplify the use case, a “DAY’ in the hotel room starts from 00:00 to 23:59:59.
- Every end-user can check the room availability, place a reservation, cancel it or modify it.
- To simplify the API is insecure.

To Debug this Application Open the azure Cosmos DB Emulator and create a DB Called BookingDb and configure the aplication on VS to RUN on IIS Express (in case that you have a Cosmos DB runing  on cloud you can just Change the conexion properties on the appsettings.js file and run it direclty on the container).

The solution has the following Structure:

![image](https://user-images.githubusercontent.com/7016922/143625166-1f59220d-a172-44f7-9795-09667e8b83b9.png)

ON Base project you can find the API with all the controllers and configuration files
On Core project you can find all the services and the business logic from the application
On Data Project you can find all the repositories and data access layer
On infraestructure you can find helpers, atributes, extention methods, Etc... used for the other layers on the application
On test proyect you can find some examples of the unit testing that the aplication probably require
