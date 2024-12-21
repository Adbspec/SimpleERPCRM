# Setting Up Entity Framework Core with Pomelo MySQL Provider

## Introduction
This guide explains how to set up Entity Framework Core with Pomelo MySQL Provider using Scaffold-DbContext command.

## Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download) installed
- Pomelo.EntityFrameworkCore.MySql package installed
- Access to a MySQL database

## Instructions
1. Open a terminal or command prompt.
2. Run the following Scaffold-DbContext command, replacing the connection string with your own:
    ```bash
    Scaffold-DbContext "Server=localhost;Port=3307;Database=erpcrm;User=root;Password=root;Persist Security Info=False;Allow Zero Datetime=True;Connect Timeout=300" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -f -Context DBContext
    ```
3. This command will generate the required models based on your database schema in the specified output directory.

## How to run the application



## Additional Notes
- Ensure that you have proper permissions to access the database.
- Adjust the command parameters as per your specific database configuration.
- Make sure to replace sensitive information like passwords before sharing or committing to version control.

For more information on Pomelo MySQL Provider, refer to the [official documentation](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql).
