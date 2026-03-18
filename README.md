# The IT Book Online Shop (API)

A RESTful API built with ASP.NET Core (.NET 8) for an IT bookstore. This application allows users to register, log in, browse IT books (fetched from Google Books API), and save their favorite books to a MySQL database.

## 🚀 Features
- **User Authentication:** Register and Login securely.
- **Book Catalog:** Browse a list of IT books related to "MySQL", sorted alphabetically by title.
- **Favorite Books:** Users can "like" books to save them to their profile.
- **Third-Party Integration:** Real-time book data fetching using Google Books API.

## 🛠️ Technologies & Libraries Used
- **Framework:** .NET 8 (ASP.NET Core Web API)
- **Database:** MySQL
- **ORM:** Entity Framework Core
- **Third-Party Libraries:** - `Pomelo.EntityFrameworkCore.MySql` (MySQL Provider)
  - `Swashbuckle.AspNetCore` (Swagger UI for API documentation)

## 🏗️ Database Design
The system uses a highly efficient relational design:
- `Users` table: Stores user credentials and profile information.
- `UserLikes` table: Acts as a mapping table storing the relationship between a `UserId` and a `BookId`. 
*Note: To ensure a Single Source of Truth and optimize performance, full book details are not duplicated in the local database. Only the `BookId` is stored, while book details are dynamically fetched from the external API.*

## ⚙️ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed.
- MySQL Server installed and running.
- Visual Studio 2022 or VS Code.

## 🏃‍♂️ How to Compile and Start the Application

**Step 1: Clone the repository**
\`\`\`bash
git clone <your-repository-url>
cd TheITBookOnlineShop
\`\`\`

**Step 2: Database Configuration**
Open `appsettings.json` and update the `DefaultConnection` string with your MySQL credentials:
\`\`\`json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=bookstore;User=root;Password=your_password;"
}
\`\`\`

**Step 3: Apply Database Migrations**
If the database tables (`Users` and `UserLikes`) are not yet created, run the following command in the terminal or Package Manager Console to apply migrations:
\`\`\`bash
dotnet ef database update
\`\`\`
*(Alternatively, you can manually create the tables in your MySQL database based on the entities).*

**Step 4: Run the Application**
Execute the following command to start the API:
\`\`\`bash
dotnet run
\`\`\`

**Step 5: Test the APIs**
Once the application is running, a browser window will automatically open the **Swagger UI** (e.g., `https://localhost:<port>/swagger`). You can use Swagger or Postman to test the following endpoints:

- `POST /register` - Register a new user.
- `POST /login` - Authenticate a user.
- `GET /books` - Fetch and list books (sorted A-Z).
- `POST /user/like` - Like a specific book.

## 🛡️ Error Handling
The API includes comprehensive error and exception handling. It utilizes `try-catch` blocks and `ILogger` for server-side logging. Appropriate HTTP status codes (200, 400, 404, 500) are returned based on the scenario (e.g., returning 400 Bad Request if a username is already taken or if a user likes the same book twice).
