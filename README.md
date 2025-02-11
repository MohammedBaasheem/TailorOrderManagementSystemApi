

## Tailor Order Management System API

This Project describes the API for a Tailor Order Management System, enabling users to manage fabrics, orders, and user accounts.  The API is built using ASP.NET Core and follows RESTful principles.

### Technologies Used

*   ASP.NET Core 7.0 (or latest)
*   Entity Framework Core (for database access)
*   Microsoft Identity Framework (for authentication and authorization)
*   AutoMapper (for object-object mapping)
*   Swashbuckle/OpenAPI (for API documentation and testing)
*   SQL Server (or other database of your choice)

### API Overview

The API is designed with a layered architecture, separating concerns for maintainability and scalability.

*   **Controllers:** Handle incoming HTTP requests and route them to the appropriate services.
*   **Services:** Contain the core business logic of the application.
*   **Repositories (Data Access Layer):**  Abstract database interactions, providing a clean interface for services.  (Consider using the Repository Pattern explicitly for better testability).
*   **Models:** Define the data structures (entities and DTOs) used throughout the application.

### Authentication and Authorization

The API uses JWT (JSON Web Tokens) for authentication and authorization.  Users can register, log in, and receive a JWT for accessing protected endpoints.  Role-based authorization is implemented to restrict access to specific functionalities based on user roles (e.g., Administrator, Tailor, Customer).

*   **Registration:**  `POST /api/auth/register`
*   **Login:** `POST /api/auth/login`
*   **Token Refresh:** `POST /api/auth/refresh-token` (using refresh tokens stored securely, ideally in a database associated with the user)

### API Endpoints

The following sections detail the available API endpoints.  Each endpoint includes the HTTP method, route, request body (if applicable), response status codes, and example responses.

#### Fabric Management

*   **Get All Fabrics:** `GET /api/fabrics`
    *   Response: `200 OK` - Array of fabric objects.
    *   Example:
        ```json
        [
          { "id": 1, "name": "Cotton", "description": "Soft and breathable", "pricePerMeter": 10.00 },
          { "id": 2, "name": "Linen", "description": "Lightweight and durable", "pricePerMeter": 15.00 }
        ]
        ```
*   **Get Fabric by ID:** `GET /api/fabrics/{id}`
    *   Response: `200 OK` - Fabric object.  `404 Not Found` - If fabric doesn't exist.
*   **Create Fabric:** `POST /api/fabrics`
    *   Request Body: Fabric object (name, description, pricePerMeter).
    *   Response: `201 Created` - Fabric object with the newly generated ID.
*   **Update Fabric:** `PUT /api/fabrics/{id}`
    *   Request Body: Updated fabric object.
    *   Response: `204 No Content` - On successful update. `404 Not Found` - If fabric doesn't exist.
*   **Delete Fabric:** `DELETE /api/fabrics/{id}`
    *   Response: `204 No Content` - On successful deletion. `404 Not Found` - If fabric doesn't exist.

#### Order Management

*   **Get All Orders (with Filtering and Sorting):** `GET /api/orders`
    *   Query Parameters:  `page`, `pageSize`, `sortBy`, `sortDirection` (e.g., `asc`, `desc`), `filter` (using a filter expression or a library like Sieve).
    *   Response: `200 OK` - Paginated list of orders.
*   **Get Order by ID:** `GET /api/orders/{id}`
    *   Response: `200 OK` - Order object. `404 Not Found` - If order doesn't exist.
*   **Create Order:** `POST /api/orders`
    *   Request Body: Order details (customer ID, fabric ID, measurements, description, etc.).
    *   Response: `201 Created` - Order object with the newly generated ID.
*   **Update Order Status:** `PATCH /api/orders/{id}/status`  (Use PATCH for partial updates like status changes)
    *   Request Body:  `{ "status": "InProgress" }`
    *   Response: `204 No Content` - On successful update. `404 Not Found` - If order doesn't exist.
*   **Delete Order:** `DELETE /api/orders/{id}`
    *   Response: `204 No Content` - On successful deletion. `404 Not Found` - If order doesn't exist.

#### User Management

*   **Register User:** `POST /api/users/register`
*   **Get User by ID:** `GET /api/users/{id}` (Protected endpoint - requires authentication)
*   **Update User:** `PUT /api/users/{id}` (Protected endpoint)
*   *(Other user management endpoints as needed)*

### Error Handling

The API uses global exception handling middleware to provide consistent error responses in JSON format.

```json
{
  "statusCode": 500,
  "message": "Internal Server Error",
  "details": "Detailed error message (for debugging in development)"
}
```

### API Documentation (Swagger/OpenAPI)

The API includes Swagger/OpenAPI documentation, accessible at `/swagger`, which allows you to explore the API, test endpoints, and generate client SDKs.

### Getting Started (Development)

1.  Clone the repository.
2.  Restore NuGet packages.
3.  Configure the database connection string.
4.  Apply database migrations.
5.  Run the API.

### Contributing

(Add contribution guidelines if applicable.)

This revised documentation is more detailed, provides concrete examples, and follows best practices for API documentation.  Remember to keep your API documentation up-to-date as your API evolves.  Using a tool like Swashbuckle/OpenAPI is highly recommended for automatic documentation generation.
