## Requirements
- Visual studio 2022
- NodeJS LTS (v20.12.2)
- MSSQL installed and running or a docker [image for mssql](https://hub.docker.com/_/microsoft-mssql-server)

## Instructions WEB
- Create a self-signed certificate at the root of the repository like this
```
dotnet dev-certs https -ep ./development_cert.pem --format Pem -np
```
- Open folder **code/frontend/fakemyresume**
- Run **npm install**
- Run **npm run watch** or **npm run build**

## Instructions API
- Load solution **code/backend/FakeMyResume.sln** in Visual studio.
- Adjust database connection strings if required. (See Database and Quartz setup sections below)
- Run the default https configuration from Visual studio.

## Database setup
- Create a database in your MSSQL instance for storing application data.
- To match the default development configuration use database name **MakeMyResume**

## Quartz setup
- Create a database in you MSSQL instance to be used for Quartz clustering.
- To match the default development configuration use database name **Quartz**
- Refer to [Quartz database setup](https://github.com/quartznet/quartznet/tree/main/database/tables) and run the required commands to setup the required database.
- Set user secrets **StackExchangeSettings:Url** and **StackExchangeSettings:Key** for Update tags job.

## Enable AI Features
- Set user secret **OpenAIServiceOptions:ApiKey** This can be obtained from your OpenAPI account.