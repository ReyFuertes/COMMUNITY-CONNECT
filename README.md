# Community Connect

Community Connect is a microservices-based application designed to manage users, units, pets, and vehicles within a community. It leverages .NET for backend services and Docker Compose for easy deployment and orchestration.

## Technologies Used

-   **.NET 8**: Backend services developed with ASP.NET Core.
-   **Docker / Docker Compose**: Containerization and orchestration of services.
-   **SQL Server**: Database for persistence.
-   **MediatR**: In-process messaging for a cleaner architecture.
-   **SignalR**: Real-time communication for instant messaging and alerts.
-   **JWT Authentication**: Secure API access.
-   **Swagger/OpenAPI**: API documentation and testing.

## Project Structure

-   `Gateway`: An API Gateway project.
-   `UserAndUnitManagement`: A microservice responsible for managing users, units, pets, and vehicles.
    -   `UserAndUnitManagement.Api`: The API layer.
    -   `UserAndUnitManagement.Application`: Business logic and MediatR handlers.
    -   `UserAndUnitManagement.Domain`: Domain entities and interfaces.
    -   `UserAndUnitManagement.Infrastructure`: Data access and persistence.
-   `CommunicationHub`: A microservice for announcements, messages, and emergency alerts.
    -   `CommunicationHub.Api`: The API layer hosting the SignalR Hub.
    -   `CommunicationHub.Application`: CQRS handlers and notification interfaces.
    -   `CommunicationHub.Domain`: Domain entities (Announcements, Messages).
    -   `CommunicationHub.Infrastructure`: EF Core and repository implementations.
    -   `CommunicationHub.Client`: A console application for testing real-time SignalR notifications.

## Setup Instructions

1.  **Prerequisites**:
    -   .NET 8 SDK
    -   Docker Desktop
2.  **Clone the Repository**:
    ```bash
    git clone [repository_url]
    cd community-connect
    ```
3.  **Build and Run with Docker Compose**:
    Navigate to the root directory of the project (where `docker-compose.yml` is located) and run:
    ```bash
    docker-compose --profile development up -d --build
    ```
    This command will build the Docker images and start all services.

## Running the Application

Once the Docker containers are up and running, the services will be accessible at:

-   **API Gateway**: `http://localhost:8080`
-   **UserAndUnitManagement API**: `http://localhost:8081`
-   **CommunicationHub API**: `http://localhost:8082`

## Testing Real-Time Notifications

To test the SignalR functionality:
1.  Ensure the services are running via Docker.
2.  Navigate to `CommunicationHub/CommunicationHub.Client`.
3.  Run the client: `dotnet run`.
4.  Use the Swagger UI or Gateway to send a message/announcement, and observe the client console.

## API Documentation (Swagger)

Each microservice provides Swagger UI for easy exploration:

-   **UserAndUnitManagement Swagger UI**: `http://localhost:8081/swagger`
-   **CommunicationHub Swagger UI**: `http://localhost:8082/swagger`