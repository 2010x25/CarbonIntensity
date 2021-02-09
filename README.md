# Carbon-Intensity
A C# Console application for polling https://carbon-intensity.github.io/api-definitions/#carbon-intensity-api-v2-0-0 periodically to capture carbon intensity. CO2 emissions in grams per kWh of energy generated in United Kingdom.

SQL Server is used for storing the time series data. Application store data in SQL tables available for updating (adding new time series data points) and querying. Polling is resumed from the last stored timestamp to account for the application downtime.

# Application Setup
Application is implemented in C# & .NET Core 3.1. Visual Studio 2019 Community or Visual Studio Code can be used to compile and Run. Database software is SQL server. Free version of SQL Server (SQL Express 2016) is required.
