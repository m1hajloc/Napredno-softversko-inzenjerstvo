🔗 URL Shortener

Full-stack URL shortening application built with ASP.NET Core and Angular, using SQL Server running in Docker.

📁 Project Structure
.
├── .gitignore
├── README.md
├── URLShorteningService # ASP.NET Core backend
└── urlShortenerClient # Angular frontend

🧰 Tech Stack
Backend

ASP.NET Core

Entity Framework Core

SQL Server

Docker & Docker Compose

Frontend

Angular

Angular Material

RxJS

✅ Prerequisites

Make sure you have the following installed:

.NET SDK 8+

dotnet --version

Node.js 18+

node --version

Angular CLI

npm install -g @angular/cli

Docker

docker --version

EF Core CLI

dotnet tool install --global dotnet-ef

Restart your terminal after installing dotnet-ef.

🐳 Database (SQL Server with Docker)

The project uses SQL Server running in Docker.

Start the database

From the root folder:

docker compose up -d

Verify it’s running:

docker ps

SQL Server will be available on:

localhost:51433

🗄️ Database Migrations
1️⃣ Go to backend project
cd URLShorteningService

2️⃣ Apply migrations
dotnet ef database update

If no migrations exist yet:

dotnet ef migrations add InitialCreate
dotnet ef database update

▶️ Running the Backend

From URLShorteningService:

dotnet run

Backend will start on something like:

https://localhost:5001

▶️ Running the Frontend

From urlShortenerClient:

npm install
ng serve

Frontend will be available at:

http://localhost:4200

🔐 Authentication

Authentication is currently handled via an API key sent in request headers:

X-Api-Key: <your-api-key>

(This will be improved in future iterations.)

🌍 CORS

CORS is configured to allow requests from:

http://localhost:4200

🚀 Future Improvements

Proper login & token-based authentication

Route guards in Angular

Refresh tokens

Better error handling

Analytics dashboard improvements

🛠️ Development Notes

Angular and backend are kept in the same repository for easier development

Docker is used only for infrastructure (database)

No secrets are committed to the repository

📜 License

MIT