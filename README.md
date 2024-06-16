# Viventium Take Home Project Submission

## Introduction

This repository contains the submission for the take-home project required by Viventium. The project demonstrates proficiency in C#,.NET, Entity Framework Core, and MediatR, focusing on data manipulation, database interactions, API documentation, and implementing the CQRS pattern.

## Solution Structure

The project is structured into several layers, each serving a distinct purpose, reflecting a Clean Architecture approach with a focus on CQRS (Command Query Responsibility Segregation):

### API Project

- **Purpose:** Acts as the entry point for HTTP requests, exposing RESTful APIs.
- **Features:** Integrates Swagger UI for API documentation, enabling easy exploration and testing of available endpoints.

### Application Layer

- **Purpose:** Handles business logic, orchestrating calls between the domain and infrastructure layers.
- **Components:** Contains queries, commands, handlers, and data validators for command objects. Also includes DTOs (Data Transfer Objects) for returning query results and sending commands.

### Infrastructure Layer

- **Purpose:** Manages data persistence and external resources.
- **Components:** Includes Entity Framework configurations, migrations, and repositories for accessing the database.

### Domain Project

- **Purpose:** Defines the core business entities and rules.
- **Components:** Contains anemic domain models, representing the structure of the data without business logic, as per the project instructions.

## Setup and Configuration

### Running the Application

1. Clone the repository:
`git clone https://github.com/ijabit/viventium-takehome-project.git`

2. Open the project in your preferred IDE.

3. Build and run the application:
`dotnet build`
`dotnet run`


Upon running the project in Visual Studio, an SQLite database will be automatically provisioned (if necessary) by running an EF migration in the startup code. The included "hrdatabase.db" file has been pre-created in source control with all of the data from the provided DATA.csv, given to me by Viventium.

## Features

- **Swagger UI Documentation:** Provides an interactive documentation portal for the API endpoints.
- **CQRS Implementation:** Utilizes MediatR to implement the CQRS pattern, separating read and write operations for a cleaner and more maintainable architecture.
- **Efficient Data Operations:** Leverages EF Core for efficient data retrieval and manipulation, including bulk operations.
- **Modular Architecture:** Emphasizes separation of concerns and scalability through a CQRS design.

---

Thank you for taking the time to review my project. Your feedback is greatly appreciated!

---