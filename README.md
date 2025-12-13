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
-   `MaintenanceService`: A microservice for handling work orders, maintenance requests, and issue tracking.
    -   `MaintenanceService.Api`: The API layer.
    -   `MaintenanceService.Application`: Business logic and MediatR handlers for work orders.
    -   `MaintenanceService.Domain`: Domain entities (WorkOrder, Attachments) and enums.
    -   `MaintenanceService.Infrastructure`: EF Core persistence and repositories.
-   `FinanceService`: A microservice for managing financials, including billing, payments, and expense tracking.
    -   `FinanceService.Api`: The API layer, handling payment initiation and webhooks.
    -   `FinanceService.Application`: Business logic for payment processing, invoicing, and expense management.
    -   `FinanceService.Domain`: Domain entities (Invoice, Payment, Expense) and interfaces.
    -   `FinanceService.Infrastructure`: Data access, payment gateway integrations (Stripe, Xendit), and Azure Blob Storage for receipts.
-   `SecurityService`: A microservice for visitor management, access control, and parcel tracking.
    -   `SecurityService.Api`: The API layer, providing endpoints for guards and residents.
    -   `SecurityService.Application`: Business logic for visitor passes, QR code generation, and parcel logging.
    -   `SecurityService.Domain`: Domain entities (VisitorPass, VisitLog, Parcel).
    -   `SecurityService.Infrastructure`: EF Core persistence, QR code generation service, and notification client.
-   `BookingService`: A microservice for managing facility and amenity bookings.
    -   `BookingService.Api`: The API layer for amenity and booking management.
    -   `BookingService.Application`: Business logic for creating bookings, applying rules, and managing amenities.
    -   `BookingService.Domain`: Domain entities (Amenity, Booking, BookingRule).
    -   `BookingService.Infrastructure`: EF Core persistence, integration clients for FinanceService and CommunicationHub.

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
-   **MaintenanceService API**: `http://localhost:8083`
-   **FinanceService API**: `http://localhost:8084`
-   **SecurityService API**: `http://localhost:8085`
-   **BookingService API**: `http://localhost:8086`

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
-   **MaintenanceService Swagger UI**: `http://localhost:8083/swagger`
-   **FinanceService Swagger UI**: `http://localhost:8084/swagger`
-   **SecurityService Swagger UI**: `http://localhost:8085/swagger`
-   **BookingService Swagger UI**: `http://localhost:8086/swagger`

## FinanceService Specifics

The FinanceService handles all financial transactions, including online payments and expense tracking.

### Payment Gateway Configuration

For online payments to function, you must configure the API keys for Stripe and Xendit (for local e-wallets like GCash, PayMaya, GrabPay, GoTyme). These settings are found in `FinanceService/FinanceService.Api/appsettings.Development.json` and should be managed securely in production environments (e.g., Kubernetes secrets, Azure Key Vault).

Example `appsettings.json` (replace placeholders with actual keys):

```json
{
  "Stripe": {
    "SecretKey": "sk_live_YOUR_STRIPE_SECRET_KEY",
    "SuccessUrl": "http://yourfrontend.com/payment/success",
    "CancelUrl": "http://yourfrontend.com/payment/cancel"
  },
  "Xendit": {
    "SecretKey": "xnd_live_YOUR_XENDIT_SECRET_KEY",
    "SuccessUrl": "http://yourfrontend.com/payment/success",
    "FailureUrl": "http://yourfrontend.com/payment/failure"
  }
}
```

### Azure Blob Storage for Proofs of Payment

The FinanceService uses Azure Blob Storage for storing uploaded proofs of payment (e.g., bank transfer receipts).

Configure your Azure Storage connection string and container name in `FinanceService/FinanceService.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "AzureStorage": "DefaultEndpointsProtocol=https;AccountName=<YOUR_ACCOUNT_NAME>;AccountKey=<YOUR_ACCOUNT_KEY>;EndpointSuffix=core.windows.net"
  },
  "AzureStorage": {
    "ContainerName": "payment-proofs"
  }
}
```

*   **`ConnectionStrings:AzureStorage`**: Your Azure Storage account connection string. For local development, `UseDevelopmentStorage=true` can be used with Azurite.
*   **`AzureStorage:ContainerName`**: The name of the blob container where receipts will be stored (defaults to `payment-proofs`).

### Webhook Endpoints

To receive real-time payment status updates, you must configure webhooks with your payment providers:

*   **Stripe**: Configure a webhook endpoint pointing to `http://localhost:8080/api/finance/webhooks/stripe` (adjust for production URL) for `checkout.session.completed` events.
*   **Xendit**: Configure a webhook endpoint pointing to `http://localhost:8080/api/finance/webhooks/xendit` (adjust for production URL) for `invoice paid` and `invoice expired` events.

## SecurityService Specifics

The SecurityService handles visitor management, check-ins, and parcel tracking.

### Azure Blob Storage for Parcel Photos

The SecurityService uses Azure Blob Storage to store photos of received parcels.

Configure your Azure Storage connection string and container name in `SecurityService/SecurityService.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "AzureStorage": "DefaultEndpointsProtocol=https;AccountName=<YOUR_ACCOUNT_NAME>;AccountKey=<YOUR_ACCOUNT_KEY>;EndpointSuffix=core.windows.net"
  },
  "AzureStorage": {
    "ContainerName": "security-files"
  }
}
```

*   **`ConnectionStrings:AzureStorage`**: Your Azure Storage account connection string. For local development, `UseDevelopmentStorage=true` can be used with Azurite.
*   **`AzureStorage:ContainerName`**: The name of the blob container where parcel photos will be stored (defaults to `security-files`).

### Visitor Access Code / QR Code Generation

The API generates a unique access code (string) for visitor passes. Frontend applications can then use this string to generate a scannable QR code image.

### Guard Application

Security personnel can use a dedicated application (mobile/tablet) to:
*   Scan visitor QR codes to check guests in/out.
*   Log new parcels and upload photos.

## BookingService Specifics

The BookingService manages the booking of shared community facilities and amenities.

### Integrations

*   **FinanceService**: Used for handling booking fees and security deposits. When a booking with a fee is created, the BookingService initiates a payment request to the FinanceService.
*   **CommunicationHub**: Used for sending notifications to residents regarding booking confirmations, approvals, rejections, and other status updates.
*   **UserAndUnitManagement**: Integrates with this service to validate Resident and Unit IDs when creating bookings.

### Configuration

The BookingService needs to know the base URLs of the services it integrates with. Configure these in `BookingService/BookingService.Api/appsettings.Development.json` (or environment variables for production):

```json
{
  "FinanceService": {
    "BaseUrl": "http://communityconnect-financeservice-api-dev:8084"
  },
  "CommunicationHub": {
    "BaseUrl": "http://communityconnect-communicationhub-api-dev:8082"
  },
  "UserAndUnitManagement": {
    "BaseUrl": "http://communityconnect-userandunitmgmtapi-dev:8081"
  }
}
```

*   **`FinanceService:BaseUrl`**: The base URL of the FinanceService API.
*   **`CommunicationHub:BaseUrl`**: The base URL of the CommunicationHub API.
*   **`UserAndUnitManagement:BaseUrl`**: The base URL of the UserAndUnitManagement API.

### Booking Rules Engine

Administrators can set various rules for each amenity, such as:
*   Maximum booking duration (e.g., "2 hours").
*   Capacity limits (e.g., "10 people").
*   Blackout dates (e.g., no bookings on holidays).

These rules are checked automatically when a resident attempts to create a booking.

### Approval Workflow

Some amenities can be configured to require admin approval for bookings, while others can be auto-approved.

---