# Задание на позицию Strong Junior .NET разработчика

Разработать RESTful API для управления задачами (Task Management API), используя ASP.NET Core и PostgreSQL.

##### Project Dependencies
* __Flask__ - Slim python web library.
* __SQLAlchemy__ - Python ORM library
* __RenderOption__ - PaaS platform for easy hosting of web apps
* __Postman__ - API testing tool

### Installation instructions on loccal machine
* Clone project to directory of your choice.
* Run docker-compose.yml
* Create 2 databases in postgreSQL: HangFireDB and TaskManagement
* Run script in ./SQL/ folder
* Execute ./src/TaskManagementAPI and ./src/SecondServiceConsole with ```dotnet run``` command
* Go to ./frontend direcory and execute ```npm install``` and them ```npm run dev``` (must have nodejs and npm installed)

### Features
* ./frontend - will show last tasks created by API via SignalR
* ./src/TaskManagementAPI - main API
* ./src/SecondServiceConsole - will display created tasks placed into RabbitMQ queue (consumer)
* http://localhost:5001/hangfire - recurring job will be executing every 1 min and remove one day old tasks

### Auth:
* You can auth by ```{{baseUrl}}/api/v1/auth/login``` link 
* Must provide following credentials
```
{
  "username": "admin",
  "password": "admin"
}
```
