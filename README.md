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

## API Endpoints

### Public Endpoints
* `POST /webapi/Register` - Registers a new user. [cite_start]Includes validation to ensure unique usernames[cite: 16, 17].

### Authenticated User Endpoints
* [cite_start]`GET /webapi/Donation/{amount}` - Generates a donation certificate for logged-in users.

### Staff-Only Endpoints (Basic Auth Required)
* `POST /webapi/AddEvent` - Adds a new museum event. [cite_start]Validates date formats using Regex[cite: 17, 18].
* [cite_start]`GET /webapi/EventCount` - Returns the total number of scheduled events.
* [cite_start]`GET /webapi/Event/{id}` - Retrieves a specific event formatted as an iCalendar (RFC 5545) file[cite: 18].
