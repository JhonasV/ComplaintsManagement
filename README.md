# Complaints Management 
Complaints Management system for telecomunication products

## Features
Management for:
- Customers
- Complaints status
- Complaints options
- Complaints
- Products
- Departments
- Users
- Roles

## Technologies
- ASP.NET MVC 5
- Bootstrap with Mataerial UI theme
- EntityFramework 6
- Identity Server

## Installation

Restore the dependecies in the folder `ComplaintsManagement`

```bash
dotnet restore
```
Add Sql Server connection string in  `ComplaintsManagement.UI/Web.config` file

```bash
  <connectionStrings>
    <add name="DefaultConnection" connectionString="Your Sql Server connection string here" />
  </connectionStrings>
```
Go in the root of the folder `ComplaintsManagement\ComplaintsManagement.UI` and setup the database

```bash
Enable-Migrations
```
and
```bash
Update-database
```


## Run the project
Load the project in Visual Studio

## Preview

<img
width='80%'
src='https://github.com/JhonasV/ComplaintsManagement/blob/develop/captures/cap1.PNG'/>
<br/>