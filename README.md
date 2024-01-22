# Library Web API Installation and Configuration Guide
## Installation:

### 1. Clone the Project:

```bash
git clone https://github.com/Garlsby/library-web-api.git
```

### 2. Open in Visual Studio:

- Open the project in your preferred integrated development environment (IDE), such as Visual Studio.

### 3. Open Solution File:

- Inside the project folder, open the solution file (SLN file) named SchoolAppApi.sln.

### 4. Run Dotnet Restore:
Execute using Package Manager Console in :
#### Tools > NuGet Package Manager > Package Manager Console
- In the terminal, run the following command to restore project dependencies:

```bash
dotnet restore
```

## Connection String Modification:

### 1. Update appsettings.json:

- Navigate to the appsettings.json file in your project.

- Modify or add connection strings under "ConnectionStrings":

```json

    "ConnectionStrings": {
      "DefaultConnection": "Data Source=localhost;Initial Catalog=StudentsBook;Integrated Security=True;Pooling=False;TrustServerCertificate=True"
      // Add your custom connection strings here
      // don't forget to include TrustServerCertificate=True 
    },
```
### 2. Update Program.cs:
- Go to the Program.cs file and change the GetConnectionString to your desired connection:

```csharp

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddDbContext<StudentDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
```
### 3. Before Running the App:

- Open Package Manager Console and run the following commands to apply database migrations:

```bash

Add-Migration InitialCreate -Context AppDbContext
update-database -Context AppDbContext

Add-Migration InitialCreate -Context StudentDbContext
update-database -Context StudentDbContext
```
