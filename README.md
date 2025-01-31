# PSTDotNetTrainingBatch5

C# .Net

C# Language .Net

Console App
Windows Forms (No longer still in Macos)
ASP.NET Core Web API 
ASP.NET Core Web MVC 
Blazor Web Assembly 
BLazor Web Server

.Net framework(1, 2, 3, 3.5, 4, 4.5, 4.6, 4.7, 4.8) windows .Net Core(1, 2, 2.2, 3, 3.1) vs2019, vs2022 - windows, linux, macos .Net (5 - vs2019, 6 - vs2022, 7, 8 - windows, linux, macos)

vscode visual studio 2022

windows

UI + Business logic + Data Access => Database

Kpay

Mobile No => Transfer

Mobile No check 10000

PST => Collin

10000 => 0

-5000 => +5000

Bank +5000


efcore database first (manual, auto) / code first 

dotnet ef dbcontext scaffold "Server=MSI\SQLEXPRESS2022; Database=DotNetTrainingBatch5; User Id=sa; Password=sasa; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -tblname -f

dotnet ef dbcontext scaffold "Server=MSI\SQLEXPRESS2022; Database=DotNetTrainingBatch5; User Id=sa; Password=sasa; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f


request model => 3
response model => success + Resp Code + ID
Dto

3
20 

17


--------------------------------------------

5 weeks

Visual Studio 2022 Installation 
Microsoft SQL Server 2022  ***
SSMS (SQL Server Management System) ***

C# Basic
SQL Basic

Console App (Create Project)
DTO (data transfer object)
Nuget Package
ADO.NET
Dapper
- ORM
- Data Model
- AsNoTracking
EFCore
- AppDbContext
- Database First
REST API (ASP.NET Core Web API)
- Swagger
- Postman
- Http Method
- Http Status Code

--------------------------------------------

Backend API


data model (data access, database) 10 columns 
view model (fontend return data) 2 columns


--------------------------------------------

- mssql basic
- C# basic
- Console App
- ado.net
- dapper
- efcore
- efcore database first
- northwind database ***
- asp.net core web api
- minimal api / [ado.net / dapper => custom service] (To become better current things)

.net 6
.net 7

.net core 3.1



--------------------------------------------

File.json

File.json => Read => Convert Object [] => Insert => json => write

--------------------------------------------


### Kpay

Mobile No 

Me - Another Person

User Table
ID
Full Name
Mobile No
Balance
Pin => 123456


Bank => Deposit / Withdraw

### Deposit 

Deposit Api => Mobile No, Amount (+) => 1000 + (-1000)

### Withdraw

Withdraw Api => Mobile No, Amount (+) => 1000 - (-1000)
Minimum Balance 10000

Validation
Check Balance if < 10000 => Cannot Withdraw

### Transfer

From Mobile No =>
To Mobile No => 
Amount
Pin (check validation in BLL)
Notes

validation
- Check From Mobile No exits ?
- Check To Mobile No exits ?
- From Mobile No != To Mobile No
- Pin == Pin
- Check Balance
- From Mobile No Balace - 
- To Mobile No Balance +
- Add Transaction History
- Transaction (Complete)

Transaction Table
From Mobile No
To Mobile No
Amount
Dates
Notes

4 API

- Balance

- Create Wallet User
- Login
- Change Pin
- Phone No Change
- Forget Pin
- Reset Pin
- Frist Time Login (Pin set up)