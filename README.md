# Задание на позицию Strong Junior .NET разработчика

Разработать RESTful API для управления задачами (Task Management API), используя ASP.NET Core и PostgreSQL.

### Installation instructions on loccal machine
* Clone project to directory of your choice
* Run docker-compose.yml
* Create 2 databases in postgreSQL: HangFireDB and TaskManagement
* Run script in ./SQL/ folder
* Execute ./src/TaskManagementAPI and ./src/SecondServiceConsole with ```dotnet run``` command
* Go to ./frontend direcory and execute ```npm install``` and then ```npm run dev``` (must have nodejs and npm installed)

### Features
* ./frontend - will show last tasks created by API via SignalR
* ./src/TaskManagementAPI - main API
* ./src/SecondServiceConsole - will display created tasks placed into RabbitMQ queue (consumer)
* http://localhost:5001/hangfire - recurring job will be executing every 1 min and remove one day old tasks

### Caching:
* TaskManagerAPI caching list of values, recived by ```{{baseUrl}}/api/v1/tasks```
* Keys used for caching are in list: ```['All', 'ToDo', 'InProgress', 'Done']```
* In order to check if cache exists, go to ```{{baseUrl}}/api/v1/cache/:key```


### Auth:
* You can auth by ```{{baseUrl}}/api/v1/auth/login``` link 
* Must provide following credentials:
```
{
  "username": "admin",
  "password": "admin"
}
```
