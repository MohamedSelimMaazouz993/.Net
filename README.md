# .Net

## Guide To Execute CRUD App Using .Net as Backend !

Download The Projects and follow the screenshots Down Below 


# Screenshots

## In Visual Studio :

Appsetting.json file in API  :

![appsetting json](https://user-images.githubusercontent.com/71633887/230669199-e670fe3e-6849-4eb0-8a17-8ef0d4b210f3.jpg)

IAMDbcontext file in Core :

![db context in core](https://user-images.githubusercontent.com/71633887/230669237-de3b0f52-11c0-4223-8e0d-7b8794863a95.jpg)

## Postman :

Open Postman :

![postman 1](https://user-images.githubusercontent.com/71633887/230671436-a9f8ca76-cec3-4231-b0c8-7ea98cae2ce6.JPG)

Create New Collection :

![postman 2](https://user-images.githubusercontent.com/71633887/230671575-6ca33d6d-0fbb-4674-b3f7-0e14ebc1e393.jpg)

Add New Request : 

![postman 3](https://user-images.githubusercontent.com/71633887/230671666-41a0f044-be2f-4e16-8ef4-88fa997d4b2b.jpg)

Select  a Post Request and set this URL: 

``` http://localhost:5303/User/AddUser ```

![postman 4](https://user-images.githubusercontent.com/71633887/230671703-1f3766fe-bb38-49ba-9f44-9025aeee5e88.jpg)

Select ```Body``` > ```Raw``` and change from  ```text``` to ```json``` and the Code below on the json fields :

```bash
{
    "userNom": "Tounsii",
    "userPrenom": "Aziz1",
    "userUsername": "zizou1",
    "userEmail": "aziz1@yahoo.fr",
    "userPasswordhash": "test1234",
    "userGuid": "",
    "userTel": "",
    "userAdresse": "",
    "userPhoto": "",
    "userActive": true,
    "userCreatedbyuser": null,
    "userCreatedondate": null,
    "userUpdatedbyuser": null,
    "userUpdatedondate": null,
    "usersroles": []
}
```
![postman 5](https://user-images.githubusercontent.com/71633887/230672279-fb34c771-abf6-4537-bc8f-b38fab8261d1.jpg)

## IIS Setup : 

1. Download [dotnet-hosting-7.0.4](https://mega.nz/file/iEklyLjT#0wvBbJcD9uiQOdiRLuNnfJL1KNPz8PNk457DuGbPcG0)

2. Follow The Video below : 

[the Video](https://www.youtube.com/watch?v=gSVNQXa790c)

Make Sure All of Those are ticked ( verify every  task is ticked folder like this )

![part 1](https://user-images.githubusercontent.com/71633887/230674961-84f0f697-8bad-44b6-9798-4debfdf2828b.jpg)

![part 2](https://user-images.githubusercontent.com/71633887/230675013-c84ed840-5471-43d3-a754-2fd0b19206e2.jpg)


## Run App : 

Now We Going To Run App

Click on IIS Express :

![postman 6](https://user-images.githubusercontent.com/71633887/230672596-b31110a6-251f-4fa5-8128-87cc90e49529.jpg)

Click on Continue :

![postman 7](https://user-images.githubusercontent.com/71633887/230672741-f07c92db-8adb-43c1-a475-c3d3c92d0d89.jpg)

Now We return To Postman and Send the post Request The User will be added Succesfully 

![postman 8](https://user-images.githubusercontent.com/71633887/230672780-d2d6c194-b818-482e-a573-730f7ae18a2f.jpg)
