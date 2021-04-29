# HCatalyst Technical Project

* This repository contains the technical challenge project.  It was developed using Visual Studio 2019 version 16.9.4 with the target framework of .NET Core 5.0.

*  Once the HCatalystTest.csproj is downloaded from Github, you can open the HCatalyst.sln.  It is expected that the application will run from within Visual Studio.  This project used the in memory Entity Framework for its database functionality.  The swagger documentation will appear when the application starts

* The application will seed the database with 16 entries.  Once the application is running you can then run the Powershell script.  Execute the Powershell script in the following manner:

     powershell -executionpolicy bypass ".\TestAPI.ps1"

The parameters allows an unsigned script to run.  The "TestAPI.ps1" file will be in the same folder as the HCatalyst.csproj file.

* Automated integration testing has been created for the REST APIs.  The test functions require the application to run.  You can do this by going to the command prompt when the HCatalyst.csproj is located.  Then execute the statement "dotnet run" for the automated tests.

* The "Delay" query string parameter will add the given delay.  If omitted, there will not be any delay added in the call.

* Side note:  I wasn't sure if the 'Age' field was the correct item to store.  I was going to store the 'Date of Birth' but I ran out of time.

