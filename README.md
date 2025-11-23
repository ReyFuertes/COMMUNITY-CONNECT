# Community Connect

Community Connect is a microservices-based application designed to manage users, units, pets, and vehicles within a community. It leverages .NET for backend services and Docker Compose for easy deployment and orchestration.

## Technologies Used

-   **.NET 8**: Backend services developed with ASP.NET Core.
-   **Docker / Docker Compose**: Containerization and orchestration of services.
-   **SQL Server**: Database for persistence.
-   **MediatR**: In-process messaging for a cleaner architecture in the UserAndUnitManagement service.
-   **JWT Authentication**: Secure API access.
-   **Swagger/OpenAPI**: API documentation and testing.

## Project Structure

-   `Gateway`: An API Gateway project.
-   `UserAndUnitManagement`: A microservice responsible for managing users, units, pets, and vehicles.
    -   `UserAndUnitManagement.Api`: The API layer for the User and Unit Management service.
    -   `UserAndUnitManagement.Application`: Application layer containing business logic and MediatR handlers.
    -   `UserAndUnitManagement.Domain`: Domain layer with entities and interfaces.
    -   `UserAndUnitManagement.Infrastructure`: Infrastructure layer handling data access and persistence.

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
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build
    ```
    This command will build the Docker images (if not already built) and start all the services defined in the `docker-compose.yml` file.

## Running the Application

Once the Docker containers are up and running, the services will be accessible at the following addresses:

-   **UserAndUnitManagement API**: `http://localhost:8081`
-   **API Gateway**: `http://localhost:8080`

## API Documentation (Swagger)

The UserAndUnitManagement API provides Swagger UI for easy exploration and testing of its endpoints.

-   **UserAndUnitManagement Swagger UI**: `http://localhost:8081/swagger`

Navigate to the above URL in your browser to access the API documentation.