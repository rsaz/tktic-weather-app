# Thinkitic Weather Api and Web App

## Date

> **_Submission:_** Friday, March 5, 2021

## Deployment Location

## Project Time Spent

> **_Total:_** 6 hours
>
> - Application architecture decisions : 15min
> - Coding Api : **_1h_**
> - Coding Web app: **_1,5h_**
> - Stretch goals: **_2h_**
> - Git and Readme: **_0,5h_**
> - Deployment: **_0,1h_**
> - Recording and editing explanation video: **_0,9h_**

## Assumption

> - Since was just one api call I wondered If I could go for RPC implementation. Having just one action doesn't give enough information if the API will be data driven or action drive
> - Which json specification to use, JSON API, Schema, ION. I decided to use ION for it's simplicity, and of course based on the return json structure from the openweatherapi
> - When building a webapi these are important decisions that needs to be discussed on how far the application will evolve and grow

## Shortcuts/Compromises made

> - Did not use any shortcut. Tried to use the best of my knowledge and practices to build a clean, organized, scalable web api. But of course there are always room for improvements.

## Stretch goals

> - Could have built the frontend using razor pages which is c# embedded in web pages making the development more fluid and faster for the coding perspective. I look at the design perspective as well, so I decided to create the web app using react, consuming the web api done in C#. It was a great opportunity to refresh my knowledge with react framework. As react is widely used for front end developers, I am sure more than razor pages, also react offers the dynamism web apps require in the front end, I decided that the application should have these independent scopes to better segregate the teams and skills. Giving to the front independency.
> - Authentication was added to the service as well, using APIKey policies in the .net core framework
> - The Api can be consumed via curl, web and direct call when using authentication passing the valid key using queries
> - Api Unit test implementation using xUnit
> - Api documentation using swagger
> - Api versioning using Microsoft.Versioning package
> - Application deployment in azure
> - Explanatory video

## Instructions to run the assignment locally

### WebApi in .net core

```
1. Make sure you have installed .net core 5 on your machine
2. Run in the shell `dotnet --version` to confirm your .net framework
3. If you don't have visual studio and just want to launch the api go to the folder weatherServices
    - Folder where contains .csproj and run the command `dotnet run watch`.
    -  It will launch the webserver listening to `https://localhost:44324`
```

[.NET Core 5 Download](https://dotnet.microsoft.com/download/dotnet/5.0)

### Web app in react

```
1. Run npm install to download the dependencies
2. You need to setup `.env` file with the APIs keys as `Environment Variables` to make this project work.
    - REACT_APP_MAP_TOKEN=YOUR_MAPBOX_KEY
3. Execute npm start to launch the web app
```

#### Disclaimer

`WARNING`: Do not store any secrets (such as private API keys) in your React app or Web Api!

Environment variables are embedded into the build, meaning anyone can view them by inspecting your app's files.

- Reac web app: `I am using .env`
- Weapi in .net core: `I am using appsettings`

## What did I not include in the solution?

> - I have not implemented jest or any other test library for react

## Other information that I feel it's important to show

> - N/A

## Feedback about the challenge

> - Maybe clarify the authentication model expected. Other than that, great challenge! Helped to review some of the basic web concepts, as well as to force the dev's to stretch to other directions for experimentation
