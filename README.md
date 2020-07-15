# SimpleCommerce

A simple e-commerce application built with ASP.Net Core MVC, Entity Framework, SQL server (localdb), JQuery, Bootstrap and DataTables.
The application consists of one project named SimpleCommerce. 

## Project Status

This project is under development and currently the follwoing features are available:
- Authentication/Authorization (using Microsoft.AspNetCore.Identity and local database for storing user/roles etc)
- Admin area with product management section where the user can Add/Edit/Delete products (only accessible when logged in)
- A List Products page that is paginated where:
  - Products are searchable by Name and Description. 
  - Products can be sorted by Name, Description, Price and Date Added
  - User can add items to a shopping cart by pressing an "Add to basket" button
- A shopping cart is implemented and stored in HttpContext.Session (only in-memory)

## Setup Instructions

- Clone this repository. 
- You will need Visual Studio 2019 on your machine.  
- The application is using SQL Server Express LocalDB instance MSSQLLocalDB. Please make sure it is setup.
  - The default database name is `SimpleCommerce`
  - On startup the database will be created and seeded with user, role and product data.
  - You can configure other Sql Server data soure that LocalDB by altering the `SimpleCommerceConnection` string in `appsettings.json`.

### To Start Running:

- Open SimpleCommerce.sln in Visual Studio 2019.
- Press F5 or Debug -> Start Debugging
- The default user for logging in to the Admin section is:
  - Username: `admin@simplecommerce.com`
  - Password: `Supersecr3t`
- Happy debugging! :-)

### To Visit App:
- Open your favourite browser and go to https://localhost:5001
