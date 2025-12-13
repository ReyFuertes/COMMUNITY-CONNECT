# Community Connect Developer Wiki

## Table of Contents
1.  [Introduction](#1-introduction)
2.  [General Project Setup](#2-general-project-setup)
3.  [Core Architectural Principles](#3-core-architectural-principles)
    3.1. [Clean Architecture](#31-clean-architecture)
    3.2. [CQRS with MediatR](#32-cqrs-with-mediatr)
    3.3. [Repository Pattern](#33-repository-pattern)
    3.4. [Dependency Injection](#34-dependency-injection)
    3.5. [AutoMapper & FluentValidation](#35-automapper--fluentvalidation)
4.  [API Gateway (YARP)](#4-api-gateway-yarp)
5.  [Database Management (EF Core)](#5-database-management-ef-core)
6.  [Microservice Details](#6-microservice-details)
    6.1. [UserAndUnitManagement Service](#61-userandunitmanagement-service)
    6.2. [CommunicationHub Service](#62-communicationhub-service)
    6.3. [MaintenanceService](#63-maintenanceservice)
    6.4. [FinanceService](#64-financeservice)
    6.5. [SecurityService](#65-securityservice)
    6.6. [BookingService](#66-bookingservice)
7.  [Development Workflow](#7-development-workflow)
8.  [Troubleshooting](#8-troubleshooting)

---

## 1. Introduction

Welcome to the Community Connect Developer Wiki! This document serves as a comprehensive guide for developers working on the Community Connect microservices application. It covers the overall architecture, setup instructions, core principles, and detailed information about each microservice.

Community Connect is a microservices-based application designed to manage various aspects of a community, including user and unit management, communication, maintenance, financial transactions, and security/visitor management. It leverages .NET for backend services and Docker Compose for easy deployment and orchestration.

## 2. General Project Setup

To get started with Community Connect, follow these steps to set up your development environment.

### Prerequisites

*   **.NET 8 SDK**: Required for building and running the .NET projects.
*   **Docker Desktop**: For containerization and orchestration of all services.
*   **Git**: For version control.
*   **Editor/IDE**: Visual Studio Code, Visual Studio, or Rider are recommended.

### Cloning the Repository

1.  Open your terminal or Git client.
2.  Navigate to your desired development directory.
3.  Clone the repository:
    ```bash
    git clone [repository_url]
    cd community-connect
    ```

### Building and Running with Docker Compose

Community Connect uses Docker Compose to manage its microservices.

1.  **Navigate to the Root Directory**: Ensure you are in the `community-connect` root directory (where `docker-compose.yml` is located).
2.  **Build and Start Services**:
    ```bash
    docker-compose --profile development up -d --build
    ```
    This command will:
    *   `--profile development`: Activate the development profile, which includes development-specific configurations.
    *   `up`: Create and start the containers.
    *   `-d`: Run containers in detached mode (in the background).
    *   `--build`: Build (or rebuild) the Docker images for all services.
3.  **Verify Services**: To check if containers are running:
    ```bash
    docker-compose ps
    ```
4.  **Stop Services**: To stop and remove containers, networks, and volumes:
    ```bash
    docker-compose down
    ```

### Accessing Services

Once Docker Compose is running, the services will be accessible:

*   **API Gateway**: `http://localhost:8080` (This is the primary entry point for all client applications)
*   **UserAndUnitManagement API**: `http://localhost:8081`
*   **CommunicationHub API**: `http://localhost:8082`
*   **MaintenanceService API**: `http://localhost:8083`
*   **FinanceService API**: `http://localhost:8084`
*   **SecurityService API**: `http://localhost:8085`
*   **BookingService API**: `http://localhost:8086`

## 3. Core Architectural Principles

Community Connect adheres to several architectural principles to ensure maintainability, scalability, and testability.

### 3.1. Clean Architecture

Each microservice is structured according to the principles of Clean Architecture, organizing code into distinct layers with clear dependencies.

*   **Domain Layer**:
    *   **Purpose**: Contains the core business logic, entities, value objects, and interfaces. It is independent of all other layers.
    *   **Contents**: `Entities`, `Enums`, `Interfaces` (e.g., `IRepository<T>`).
    *   **Dependencies**: None (pure C#).
*   **Application Layer**:
    *   **Purpose**: Orchestrates the domain entities to perform application-specific business rules. It contains DTOs, commands, queries, and their handlers.
    *   **Contents**: `Dtos`, `Features` (for CQRS), `Interfaces` (e.g., `INotificationClient`, `IFileStorageService`).
    *   **Dependencies**: Depends on the `Domain` layer.
*   **Infrastructure Layer**:
    *   **Purpose**: Handles external concerns such as database access, external APIs, file storage, and specific framework implementations.
    *   **Contents**: `Persistence` (EF Core `DbContext`, Migrations), `Repositories` (implementations of `IRepository<T>`), `Services` (implementations of application interfaces like `AzureBlobStorageService`, `QrCodeService`), `Strategies` (e.g., `StripePaymentStrategy`).
    *   **Dependencies**: Depends on `Application` and `Domain` layers.
*   **API Layer**:
    *   **Purpose**: The entry point of the application, responsible for exposing RESTful endpoints, handling HTTP requests, and configuring the dependency injection container.
    *   **Contents**: `Controllers`, `Program.cs`, `appsettings.json`, Swagger configuration.
    *   **Dependencies**: Depends on `Application` and `Infrastructure` layers.

### 3.2. CQRS with MediatR

Command Query Responsibility Segregation (CQRS) is implemented using the [MediatR](https://github.com/jbogard/MediatR) library. This pattern separates the operations that change state (Commands) from the operations that read state (Queries).

*   **Commands**: Objects representing actions to be performed (e.g., `CreateInvoiceCommand`, `LogParcelCommand`). They typically return `Unit` (void) or a new entity's ID.
*   **Queries**: Objects representing requests for data (e.g., `GetInvoicesByResidentQuery`). They typically return DTOs.
*   **Handlers**: Classes that implement `IRequestHandler<TRequest, TResponse>` (for both Commands and Queries), containing the specific logic to process the request.
*   **Folder Structure**: Commands and Queries are organized under `Application/Features/[FeatureName]/Commands` and `Application/Features/[FeatureName]/Queries`.

### 3.3. Repository Pattern

A generic `IRepository<T>` interface is defined in the `Domain` layer, providing common CRUD operations. Specific repository interfaces (e.g., `IInvoiceRepository`) extend this for feature-specific queries. Implementations reside in the `Infrastructure` layer (e.g., `GenericRepository<T>`, `InvoiceRepository`).

### 3.4. Dependency Injection

.NET's built-in Dependency Injection container is used throughout the application. Services are registered in `ServiceCollectionExtensions.cs` files within the `Application` and `Infrastructure` layers, promoting loose coupling.

### 3.5. AutoMapper & FluentValidation

*   **AutoMapper**: Used for object-to-object mapping, typically between domain entities and DTOs (e.g., `Invoice` to `InvoiceDto`). Mapping profiles (`MappingProfile.cs`) are defined in the `Application` layer.
*   **FluentValidation**: Provides a clean way to define validation rules for commands and queries in the `Application` layer, ensuring data integrity before processing by handlers.

## 4. API Gateway (YARP)

The `Gateway` project uses [YARP (Yet Another Reverse Proxy)](https://microsoft.github.io/reverse-proxy/) as the API Gateway. It acts as the single entry point for all client requests, routing them to the appropriate backend microservice.

*   **Configuration**: Routing rules and cluster definitions are configured in `Gateway/appsettings.json`.
*   **Adding New Routes**: When a new microservice is added, you must:
    1.  Define a new **Route** (e.g., `securityservice-route`) with a `Path` match (e.g., `/api/security/{**catch-all}`) and a `ClusterId`.
    2.  Define a new **Cluster** (e.g., `securityservice-cluster`) with `Destinations` pointing to the Docker service name and port (e.g., `http://communityconnect-securityservice-api-dev:8085`).
    3.  Update the `docker-compose.yml` `depends_on` section for the Gateway to include the new service.

## 5. Database Management (EF Core)

Each microservice manages its own dedicated SQL Server database using Entity Framework Core.

*   **`DbContext`**: Each service has its own `DbContext` (e.g., `SecurityDbContext`) in its `Infrastructure/Persistence` folder.
*   **Migrations**:
    *   To create a new migration for a service:
        ```bash
        dotnet ef migrations add [MigrationName] --project [Service.Infrastructure.csproj] --startup-project [Service.Api.csproj]
        ```
    *   To apply pending migrations to the database:
        ```bash
        dotnet ef database update --project [Service.Infrastructure.csproj] --startup-project [Service.Api.csproj]
        ```
        *(Note: In a Docker setup, migrations are often applied automatically on container startup using code in `Program.cs` or via `entrypoint.sh` scripts)*.
*   **Connection Strings**: Database connection strings are configured in `appsettings.json` and `appsettings.Development.json` for each service, and overridden by environment variables in `docker-compose.yml`.

## 6. Microservice Details

### 6.1. UserAndUnitManagement Service

*   **Overview**: Manages users (residents, property managers, guards), units, pets, and vehicles within the community. It's also responsible for authentication and authorization.
*   **Core Entities**: `User`, `Unit`, `Pet`, `Vehicle`, `UserUnit`.
*   **Key Features**: User registration, login, profile management, unit assignment, CRUD for pets and vehicles.
*   **Authentication**: JWT-based authentication.
*   **Configuration**: Primarily `ConnectionStrings:DefaultConnection` for database, and `JwtSettings` for JWT token generation/validation.
*   **Endpoints**: `http://localhost:8081` (direct), `http://localhost:8080/userandunitmanagement` (via Gateway).

### 6.2. CommunicationHub Service

*   **Overview**: Provides real-time communication capabilities for announcements, direct messaging, and emergency alerts using SignalR.
*   **Core Entities**: `Announcement`, `Message`, `EmergencyAlert`, `Conversation`.
*   **Key Features**: Create/send announcements, real-time chat, push emergency alerts to residents.
*   **Integrations**: SignalR for real-time client communication. Other services can trigger notifications by calling its API.
*   **Configuration**: `ConnectionStrings:DefaultConnection`, `JWTSETTINGS__SECRET`.
*   **Endpoints**: `http://localhost:8082` (direct), `http://localhost:8080/api/communication` and `http://localhost:8080/hubs/communication` (via Gateway).

### 6.3. MaintenanceService

*   **Overview**: Handles work orders, maintenance requests, and issue tracking for the community.
*   **Core Entities**: `WorkOrder`, `WorkOrderAttachment`, `WorkOrderNote`.
*   **Key Features**: Residents can submit work orders, attach files, track status. Maintenance staff can be assigned, update status, and add notes.
*   **Configuration**: `ConnectionStrings:MaintenanceDbConnection`.
*   **Endpoints**: `http://localhost:8083` (direct), `http://localhost:8080/api/maintenance` (via Gateway).

### 6.4. FinanceService

*   **Overview**: Manages all financial transactions including billing, online payments, and expense tracking.
*   **Core Entities**: `Invoice`, `InvoiceItem`, `Payment`, `Expense`.
*   **Key Features**:
    *   **Invoicing**: Admins can create and manage invoices (Statements of Account).
    *   **Online Payments**: Residents can initiate payments via various methods.
    *   **Payment Tracking**: Records payment history and receipts.
    *   **Delinquency Tracking**: Flags overdue accounts.
    *   **Expense Tracking**: Admins can log community expenses.
*   **Integrations**:
    *   **Stripe**: For credit/debit card payments.
    *   **Xendit**: For local e-wallets like GCash, PayMaya, GrabPay, GoTyme, and potentially bank transfers.
    *   **Azure Blob Storage**: For storing payment proof images (e.g., bank deposit slips).
*   **Configuration**:
    *   `ConnectionStrings:DefaultConnection` (for FinanceServiceDb).
    *   `ConnectionStrings:AzureStorage` (for Azure Blob Storage).
    *   `Stripe:SecretKey`, `Stripe:SuccessUrl`, `Stripe:CancelUrl`.
    *   `Xendit:SecretKey`, `Xendit:SuccessUrl`, `Xendit:FailureUrl`.
*   **Webhook Setup**: Requires configuring webhook endpoints with Stripe and Xendit for real-time payment status updates (`/api/finance/webhooks/stripe`, `/api/finance/webhooks/xendit`).
*   **Endpoints**: `http://localhost:8084` (direct), `http://localhost:8080/api/finance` (via Gateway).

### 6.5. SecurityService

*   **Overview**: Enhances community security through visitor management, QR code-based access, and parcel tracking.
*   **Core Entities**: `VisitorPass`, `VisitLog`, `Parcel`.
*   **Key Features**:
    *   **Visitor Pre-registration**: Residents can pre-authorize guests, generating unique access codes (for QR codes).
    *   **Gate/Lobby Check-In**: Security personnel use a Guard App (consuming API) to scan QR codes, check in visitors, and log entries.
    *   **Guest Arrival Notifications**: Integrates with `CommunicationHub` to notify residents when their guests arrive.
    *   **Parcel & Delivery Management**: Guards log incoming packages, including photo evidence, generate pickup codes, and notify residents for pickup.
*   **Integrations**:
    *   **QRCoder**: For generating QR code images from access codes.
    *   **Azure Blob Storage**: For storing parcel photos.
    *   **CommunicationHub**: For sending notifications.
*   **Configuration**:
    *   `ConnectionStrings:DefaultConnection` (for SecurityServiceDb).
    *   `ConnectionStrings:AzureStorage` (for Azure Blob Storage, `security-files` container).
*   **Endpoints**: `http://localhost:8085` (direct), `http://localhost:8080/api/security` (via Gateway).

### 6.6. BookingService

*   **Overview**: Manages the fair and organized use of shared community facilities and amenities.
*   **Core Entities**: `Amenity`, `Booking`, `BookingRule`.
*   **Key Features**:
    *   **Amenity Calendar**: Provides availability information for bookable amenities.
    *   **Booking System**: Residents can reserve amenities for specific dates and time slots.
    *   **Booking Rules Engine**: Admins can set rules like booking limits, capacity limits, fees/deposits, and blackout dates.
    *   **Approval Workflow**: Some bookings may require admin approval.
*   **Integrations**:
    *   **FinanceService**: Initiates payment requests for booking fees or security deposits.
    *   **CommunicationHub**: Sends notifications for booking confirmations, status updates, and approval requests.
    *   **UserAndUnitManagement**: Validates resident and unit IDs during booking creation.
*   **Configuration**:
    *   `ConnectionStrings:DefaultConnection` (for BookingServiceDb).
    *   `FinanceService:BaseUrl`: Base URL for the FinanceService API.
    *   `CommunicationHub:BaseUrl`: Base URL for the CommunicationHub API.
    *   `UserAndUnitManagement:BaseUrl`: Base URL for the UserAndUnitManagement API.
*   **Endpoints**: `http://localhost:8086` (direct), `http://localhost:8080/api/booking` (via Gateway).

## 7. Development Workflow

### Adding a New Feature

When implementing a new feature, follow the Clean Architecture and CQRS pattern:

1.  **Domain**: Define new entities, enums, or update existing ones.
2.  **Application**:
    *   Create new Commands/Queries and their Handlers in `Features/[FeatureName]/Commands` or `Features/[FeatureName]/Queries`.
    *   Define DTOs.
    *   Implement FluentValidation for Command/Query validation.
    *   Update `MappingProfile.cs` for AutoMapper.
    *   Define new interfaces for external services (e.g., `INotificationClient`).
3.  **Infrastructure**:
    *   Update `DbContext` with new `DbSet`s, configure `OnModelCreating`.
    *   Implement concrete repositories for new entities.
    *   Implement external service interfaces (e.g., `AzureBlobStorageService` for `IFileStorageService`).
    *   Register new services in `ServiceCollectionExtensions.cs`.
    *   Create a new EF Core migration (`dotnet ef migrations add [NewFeature] --project ... --startup-project ...`).
4.  **API**:
    *   Create new API endpoints in relevant controllers (or a new controller).
    *   Inject `IMediator` and other necessary services.
    *   Call `_mediator.Send()` for commands/queries.
5.  **Integration**:
    *   If a new service, update `docker-compose.yml` and `Gateway/appsettings.json`.
    *   Update `README.md` documentation.

### Inter-Service Communication

Services communicate primarily via RESTful HTTP calls through the API Gateway. Direct service-to-service communication within the Docker network should use the Docker service name and internal port (e.g., `http://communityconnect-communicationhub-api-dev:8082`).

### Testing

*   **Unit Tests**: Focus on Domain logic, Application handlers, and Validators.
*   **Integration Tests**: Test the interaction between Application handlers and Infrastructure components (e.g., database, external services using mocks).
*   **End-to-End Tests**: Use the API Gateway to test the full request flow through multiple services.

## 8. Troubleshooting

*   **Docker Issues**:
    *   `docker-compose up -d --build` failing: Check Docker Desktop status, ensure no port conflicts, review container logs (`docker-compose logs [service_name]`).
    *   Containers restarting constantly: Check `docker-compose logs [service_name]` for errors.
    *   Database not connecting: Ensure correct connection string, DB container is healthy (`docker-compose ps`), and migrations are applied.
*   **API Gateway Routing**: If an endpoint is not reachable via the Gateway, verify `Gateway/appsettings.json` for correct `Route` and `Cluster` configurations.
*   **Database Connection**:
    *   `Server=communityconnect-db-dev` is the Docker Compose service name for the SQL Server.
    *   `User Id=sa;Password=5Tmb-_zJryCV)yLv)NNw` are the credentials defined in `docker-compose.yml`.
*   **Environment Variables**: Ensure `ASPNETCORE_ENVIRONMENT` is correctly set (e.g., `Development`) and other environment variables are passed correctly in `docker-compose.yml`.
*   **Missing Dependencies**: If a service fails to start, run `dotnet restore` on the project and then `dotnet build`. Ensure all `PackageReference` and `ProjectReference` entries in `.csproj` files are correct.
*   **Swagger Not Loading**: Check `Program.cs` for `app.UseSwagger()` and `app.UseSwaggerUI()` calls in development environment.
