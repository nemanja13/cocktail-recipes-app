# Cocktail Recipes App

This application consists of two projects, the backend in ASP.NET Core and the frontend in Angular.

The project has the following goals:

* The ASP.NET project is an independent project written in clean architecture, with code-first approach, the implementation of the basic logic was done using the Entity Framework.

* ASP.NET project implements Swagger. Which provides a user-friendly interface for exploring and testing API endpoints.

* Keep the Angular project completely separate from the ASP.NET Core code to make updates of either technology easier in the future. This was a key consideration when organizing the folders/files in the projects.

* Support running the Angular project completely separate from the ASP.NET Core Web API if desired (CORS is enabled in the Program.cs project).

## Running the Projects

To run the Angular project perform the following steps:

1. Install Node.js - https://nodejs.org

2. Install the Angular CLI:

    `npm install -g @angular/cli`

4. Open a command prompt and `cd` into the project's `cocktail-recipes-app` folder

5. Run `npm install`

6. Launch the Angular project by running `ng serve -o` 

To run the ASP.NET project perform the following steps:

1. Open SQL Server Managment Studio with SQLEXPRESS connection.

2. Open ASP.NET project and run the Migrations.

3. Then run the Api project which will open http://localhost:5000 in the browser.

4. With the help of swagger, you can start an endpoint api/faker that will fill the database with fake data using Bogus.

5. Endpoints that perform some data change are protected by authorization. JWT authorization with Bearer token is used. To get a token, you have to send a request to the endpoint "api/login", method POST with the credentials username: "admin", password: "sifra123".