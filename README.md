# 🚗 FleetManager

FleetManager is an enterprise-level ASP.NET Core MVC web application designed to streamline vehicle inventory management and track maintenance histories for commercial fleets. Built with a focus on clean architecture and robust data integrity, it provides fleet managers with a responsive, real-time dashboard to monitor their assets.

## ✨ Key Features

* **Comprehensive Vehicle CRUD:** Full lifecycle management (Create, Read, Update, Delete) for fleet vehicles, including Make, Model, VIN, and Mileage tracking.
* **Maintenance Tracking:** Deep integration with service records using Entity Framework Core eager loading to instantly view the repair history of any vehicle.
* **Soft Deletion Architecture:** Implements a strict `IsDeleted` global query filter. Vehicles are never permanently destroyed from the database, ensuring strict auditing and data retention compliance.
* **Automated Database Seeding:** Includes a built-in `DbInitializer` that automatically provisions the SQL Server database with initial test data and service logs on startup.
* **Service Layer Pattern:** Business logic is decoupled from the Controllers using a dedicated Service Layer and Repository pattern, ensuring the codebase remains highly testable and scalable.
* **Responsive Enterprise UI:** The frontend is built using standard Bootstrap 5 components, providing a clean, mobile-friendly dashboard experience.

## 🛠️ Tech Stack

* **Backend:** C#, ASP.NET Core MVC (.NET 8/Current)
* **Database:** SQL Server, Entity Framework Core
* **Frontend:** HTML5, Razor Views, Bootstrap 5, CSS3
* **Architecture:** Clean Architecture, Repository Pattern, Dependency Injection

## 🚀 Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) installed on your local machine.
* SQL Server (LocalDB or a dedicated instance) running.
* Visual Studio, VS Code, or Rider.

### Installation & Setup