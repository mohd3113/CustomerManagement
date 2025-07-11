System Setup and run.

1- Install .NET Core 8.0
2- Install Visual Studio 2022
3- Clone the repository from URL https://github.com/mohd3113/CustomerManagement.git
4- Open the solution file in Visual Studio 2022
5- Replace the appsettings.json content with the provided file content. (The database provided is a cloud-based used for test and it is working)
6- Build the solution to restore all packages and dependencies
7- Run the solution.
8- When the solution is first run, the database will be created and an admin user will be added automatically in case of a new database.
9- From Swagger UI, use the signin endpoint to log in with the admin user credentials provided.
10- After login, you will receive a JWT token. Use this token in the Authorization header of Swagger to access protected endpoints.

The complete assignment is deployed into the cloud and can be accessed at the following URL: mohd3113-002-site10.anytempurl.com 

About Solution Architecture

The solution is built using onion architecture. The solution is divided into 4 main projects (Layers):

1) API- This project contains the API controllers and handles HTTP requests. It is responsible for routing requests to the appropriate services and returning responses to the client.

2) Application- This project contains the business logic and application services. It handles the core functionality of the application, such as managing customers, orders, and products.

3) Domain- This project contains the domain entities and value objects. It defines the core business concepts and rules of the application.

4) Infrastructure- This project contains the data access layer and external services. It is responsible for interacting with the database, sending emails, and other external dependencies.


Read more about Onion Architecture at https://medium.com/expedia-group-tech/onion-architecture-deed8a554423