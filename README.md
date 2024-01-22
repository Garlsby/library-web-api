# 📖 Library Web API Installation and Configuration Guide
## Table of Contents

- [Installation](#installation)
- [Connection String Modification](#connection-string-modification)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)

# 🌐 Installation:

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

<br>

# ⚙ Connection String Modification: 

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
<br>

# 📌 API Endpoints
### 🧒 Students

| Endpoint                                 | Method | Description                                  |
| ---------------------------------------- | ------ | -------------------------------------------- |
| `/api/Admin/Students`                    | GET    | Get a list of all students.                  |
| `/api/Admin/Student/{studentId}`         | GET    | Get a specific student by ID.               |
| `/api/Admin/StudentByName/{name}`        | GET    | Get students by their name.                 |
| `/api/Admin/StudentsByDescending`        | GET    | Get a list of all students in descending order. |
| `/api/Admin/StudentBooks`                | GET    | Get information about students and the books they have borrowed. |
| `/api/Admin/CreateStudent`               | POST   | Create a new student. Requires a JSON payload with student details. |
| `/api/Admin/UpdateStudent/{studentId}`   | PUT    | Update an existing student by ID. Requires a JSON payload with updated student details. |
| `/api/Admin/DeleteStudent/{studentId}`   | DELETE | Delete a student by ID.                      |

### 📚 Books

| Endpoint                                | Method | Description                              |
| --------------------------------------- | ------ | ---------------------------------------- |
| `/api/Admin/Books`                      | GET    | Get a list of all books.                 |
| `/api/Admin/Book/{bookId}`              | GET    | Get a specific book by ID.               |
| `/api/Admin/BookByName/{name}`          | GET    | Get books by their name.                 |
| `/api/Admin/BooksByUserId/{userId}`     | GET    | Get books by the user ID who owns them.  |
| `/api/Admin/CreateBook`                  | POST   | Create a new book. Requires a JSON payload with book details. |
| `/api/Admin/ReturnBook/{bookId}`        | PUT    | Mark a book as returned by setting its StudentId to null. |
| `/api/Admin/UpdateBook/{bookId}`        | PUT    | Update an existing book by ID. Requires a JSON payload with updated book details. |
| `/api/Admin/DeleteBook/{bookId}`        | DELETE | Delete a book by ID.                      |

