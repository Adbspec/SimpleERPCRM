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

1. **Clone the Repository**  
   Clone this repository using Git:
   

2. **Install .NET 8 or higher from source**
If you don't have .NET 8 installed, you can install it by following the official installation guide. Make sure to install the correct version for your operating system.

3. **Restore Dependencies**
Run the following command to restore the project dependencies:

   ```bash
   dotnet restore
   ```
4. **Configure the Database Connection**
Open the *appsettings.json* file and replace the *DefaultConnection* connection string with your own mysql connection string.
```javascript
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3307;Database=erpcrm;User=root;Password=root;Persist Security Info=False;Allow Zero Datetime=True;Connect Timeout=300"
  }
}

```

5. **Restore the Database from dbbackup folder**
there is a  SQL backup file located in the dbbackup folder, restore the database using the MySQL command line or any MySQL workbench.

6. **Run the Application**
After configuring the connection string and restoring the database, run the application using the following command:

  ```bash
   dotnet run
   ```

7. **Default Credentials**
The default email is abc@gmail.com and the default password is 1234.


## Additional Notes
- Ensure that you have proper permissions to access the database.
- Adjust the command parameters as per your specific database configuration.
- Make sure to replace sensitive information like passwords before sharing or committing to version control.

For more information on Pomelo MySQL Provider, refer to the [official documentation](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql).
