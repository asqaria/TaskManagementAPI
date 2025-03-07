# Car Market Capstone Project

This is my final capstone project for Udacity's FullStack Web Developer Nanodegree. I careated Car Market Web Appication where users can buy or sell their cars. Web app can be accessed at [here](https://fsdu-capstone.onrender.com/)

##### Project Dependencies
* __Flask__ - Slim python web library.
* __SQLAlchemy__ - Python ORM library
* __RenderOption__ - PaaS platform for easy hosting of web apps
* __Postman__ - API testing tool

### Installation instructions on loccal machine
* Clone project to directory of your choice.
* Create a virtualenv in project directory
* run ```pip install -r requirements.txt``` to install project dependencies
* add ```DATABASE_URL``` to environment variables of your system. 
* run ```python app.py```

### Deployment instructions on Render
* Create a Render Account
* Set up a Database Service with Postgres
* Create GitHub repo
* Deploy Apps with Render's Web Service (need to connect to GitHub repo)
* Add Internal Database URL to Web Service Environment Variable
* run deploy

###Endpoints:
* GET /car and /user
* GET /cars and /users
* DELETE /car/ and /user
* POST /car and /user and
* PATCH /car/ and /user

### Roles
* Buyer
    * GET /car
	* GET /cars

* Seller
    * GET /car
	* GET /cas
	* DELETE /car
	* POST /car
	* PATCH /car
    
* Admin
	* GET /car and /user
	* GET /cars and /users
	* DELETE /car/ and /user
	* POST /car and /user and
	* PATCH /car/ and /user


### JWT Tokens for each role:
* Buyer - ```eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlJHNGlUcGQ5eFEzc0dXcmF1dThuRiJ9.eyJpc3MiOiJodHRwczovL2Rldi1zeXZvZGpjdHpoeXk2M2VpLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2NzEwNzc4YjcwMjAwMjAwNzI4NWNiYmYiLCJhdWQiOiJjb2ZmZWVzaG9wIiwiaWF0IjoxNzMwMjcyMjU3LCJleHAiOjE3MzAyNzk0NTcsInNjb3BlIjoiIiwiYXpwIjoiRDJndjViR3NYRzhuM2p3YkFvT3hRZGtRSHdnQnh5T1MiLCJwZXJtaXNzaW9ucyI6WyJnZXQ6Y2FyIiwiZ2V0OmNhcnMiXX0.myyEjiCKxrwRBw6co1mvLSjRw3d-oGnv8w8b-C9p-R4RjPu8wP_oagh_SRD8U9bDJRfXn8_NZ7hd80pVa-0dBPVd-aKtgV2NrdnNeiGK5AtJZh37YRl45o_a42J1spPJJyS3sHUjr31kO-8OVKguTOlQZ4orT7FDgAMh31ve91dJ2ArrbVTmV8l0w7-zzLVNWnpQDLEJTa5zAR3S6p3WNh4dkhYDBncfYi2gaISbLOzoU1hTIunJ0sxEcy2eNX0JEyYRXmRgPH27ZKHJn1eJXuaxxZMDM60R_4LP1T0Mgarj_ciYAkqQn692O48PZw3b4YuCj6Y5Cu-OTsOwDlLiDA```

* Seller - ```eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlJHNGlUcGQ5eFEzc0dXcmF1dThuRiJ9.eyJpc3MiOiJodHRwczovL2Rldi1zeXZvZGpjdHpoeXk2M2VpLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJhdXRoMHw2NzIxZGM2Mzg3ZDc0ZTNlY2MyZjZhMTEiLCJhdWQiOiJjb2ZmZWVzaG9wIiwiaWF0IjoxNzMwMjcyNTU5LCJleHAiOjE3MzAyNzk3NTksInNjb3BlIjoiIiwiYXpwIjoiRDJndjViR3NYRzhuM2p3YkFvT3hRZGtRSHdnQnh5T1MiLCJwZXJtaXNzaW9ucyI6WyJnZXQ6Y2FyIiwiZ2V0OmNhcnMiXX0.CzABgY7eAL5DvZIt8mYA6D_vj8bCjn_7FHrBMfGPisuFxDoc2_tMBsNagennE0m3A9Qup6hUkmoGYbaw-Vbq59my1EbLifbOnMdMRVZz9FXiZkpFjGwAgmflP1TH1Nz_F1upXEt5rcXbjKbmwjN-9Xn6NbcfrWQ_vctR75CMYVQOSU-aa8Xy7IJhQRr4-ZQWKQEK_V4lmQ5Wg73iB-UFKIufncXzFlCSawEuKzMs8dGLjL2Y5042T4K5gtSCsHSrElUOmsdv4oyEe2DGBx5gWJH5NRu7aqI4i3AQ-of9j0WjDJSTPNq_HBaUgBjmLFC9foQ8MG3W0Z8PFlJXQq7VFQ```

* Admin - ```eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IlJHNGlUcGQ5eFEzc0dXcmF1dThuRiJ9.eyJpc3MiOiJodHRwczovL2Rldi1zeXZvZGpjdHpoeXk2M2VpLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiJnb29nbGUtb2F1dGgyfDEwODA4NTg5MDI5MzY1NjU5NDc4OSIsImF1ZCI6ImNvZmZlZXNob3AiLCJpYXQiOjE3MzAyNzAwODMsImV4cCI6MTczMDI3NzI4Mywic2NvcGUiOiIiLCJhenAiOiJEMmd2NWJHc1hHOG4zandiQW9PeFFka1FId2dCeHlPUyIsInBlcm1pc3Npb25zIjpbImFkZDpjYXIiLCJhZGQ6dXNlciIsImRlbGV0ZTpjYXIiLCJkZWxldGU6dXNlciIsImdldDpjYXIiLCJnZXQ6Y2FycyIsImdldDp1c2VyIiwiZ2V0OnVzZXJzIiwidXBkYXRlOmNhciIsInVwZGF0ZTp1c2VyIl19.CJY2x5tzGrc2Z_Cb6ndWNMaWIdHnsL-2v-DkLH7rEbyQBTg9S3Y2qizvUYpeOkQ6ce8kykGvoPU3t7MWxhtQuhQESDutoLOIeHF2L4LfHcbNM7VYiwg-T_x0TZ097BjTQQhVBX3VPDotE5ACN3zFqnqN-jLHjt1JkPS8zUs6FGm1LNa6O_dVaVfjFqJ8GFNaYoT-4N2gg_4fgWTGh69CRUG6Kg3ZS-sdBPI8uCpDgFJql5ex-dZDeQWnMGMSDxLNcib0yh5oAnUORsWC25y5ehl4u3bLyUeoFBtcvyH3Oo1LD0eSNbo0_u9RpJeWHQCe-6neTp8yIQ2QUlWR6KoQ0A```

## API Endpoints

In the next few subsections, we'll cover how the API works and what you can expect back in the results.

### Default Path

#### GET /
Verifies that application is up and running on Render Option.

Sample response:
```
{
    "messaage": "healthy",
    "success": true
}
```

### GET Endpoints

#### GET /car/<int:car_id>
Displays car  in the database.

Sample response:
```
{
    "result": [
        {
            "distance": 44,
            "id": 3,
            "make": "MB",
            "model": "E class",
            "owner": 2,
            "price": "2200"
        }
    ],
    "success": true
}
```

#### GET /user/<int:user_id>
Displays user in the database.

Sample response:
```
{
    "result": [
        {
            "age": 24,
            "city": "Semey",
            "email": "duisenoglu@gmail.com",
            "gender": "Male",
            "id": 2,
            "name": "Turar"
        }
    ],
    "success": true
}
```

#### GET /cars
Displays all cars listed in the database.

Sample response:
```
{
    "result": [
        {
            "distance": 44,
            "id": 3,
            "make": "MB",
            "model": "E class",
            "owner": 2,
            "price": "2200"
        },
        {
            "distance": 44,
            "id": 4,
            "make": "MB",
            "model": "E class",
            "owner": 2,
            "price": "2200"
        }
    ],
    "success": true
}
```

#### GET /users
Displays all users listed in the database.

Sample response:
```
{
    "result": [
        {
            "age": 24,
            "city": "Semey",
            "email": "duisenoglu@gmail.com",
            "gender": "Male",
            "id": 2,
            "name": "Turar"
        },
        {
            "age": 22,
            "city": "Almaty",
            "email": "aqtos@gmail.com",
            "gender": "Male",
            "id": 4,
            "name": "Aqtos"
        }
    ],
    "success": true
}
```

### POST Endpoints

#### POST /car
Creates a new car entry in the database.

Sample response:
```
{
    "result": [
        {
            "distance": 44,
            "id": 45,
            "make": "MB",
            "model": "E class",
            "owner": 2,
            "price": "2200"
        }
    ],
    "success": true
}
```

#### POST /user
Creates a new user entry in the database.

Sample response:
```
{
    "result": [
        {
            "age": 24,
            "city": "Semey",
            "email": "duisenoglu@gmail.com",
            "gender": "Male",
            "id": 2,
            "name": "Turar"
        }
    ],
    "success": true
}
```

### PATCH Endpoints

#### PATCH /car/<int:car_id>
Updates car information given a car_id and newly updated attribute info.

Sample response:
```
{
    "result": [
        {
            "distance": 44,
            "id": 3,
            "make": "MB",
            "model": "E class",
            "owner": 2,
            "price": "2200"
        }
    ],
    "success": true
}
```

#### PATCH /user/<int:user_id>
Updates user information given a user_id and newly updated attribute info.

Sample response:
```
{
    "result": [
        {
            "age": 24,
            "city": "Semey",
            "email": "duisenoglu@gmail.com",
            "gender": "Male",
            "id": 2,
            "name": "Turar"
        }
    ],
    "success": true
}
```

### DELETE Endpoints

#### DELETE /car/<int:car_id>
Deletes a car entry from the database given the inputted car_id.

Sample response:
```
{
    "result": "deleted",
    "success": true
}
```

#### DELETE /user/<int:user_id>
Deletes an user entry from the database given the inputted user_id.

Sample response:
```
{
    "result": "deleted",
    "success": true
}
```