# Museum Management API

A RESTful backend built with **ASP.NET Core** and **Entity Framework Core** for managing museum resources, ticketing, and event scheduling.

## Key Technical Features
* **Custom Authentication:** Implemented a `Basic Authentication` handler to manage Role-Based Access Control (RBAC) for Staff and User roles.
* **Repository Pattern:** Utilized an abstraction layer for data persistence to ensure the codebase remains testable and scalable.
* **Standardized Formatting:** Developed a custom `CalendarOutputFormatter` to output event data in compliance with the **iCalendar (RFC 5545)** specification.
* **Security:** Integrated server-side regex validation and input sanitization for all API endpoints.

## Tech Stack
* **Backend:** C# / .NET
* **Database:** SQLite / Entity Framework Core
* **Tools:** Git, RESTful API Design
