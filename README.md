
# ASP.NET Core Product Management API

This ASP.NET Core solution implements a Product Management API using the CQRS (Command Query Responsibility Segregation) approach,
along with clean architecture principles. It provides CRUD operations for managing products, unit tests, Swagger documentation,
logging API requests/responses and errors, and dependency injection.


# API Endpoints
- GET /api/products: Retrieve all products.
- GET /api/products/{id}: Retrieve a product by ID.
- POST /api/products: Create a new product.
- PUT /api/products/{id}: Update an existing product.
- DELETE /api/products/{id}: Delete a product by ID.

# Usage
The API will be available at http://localhost:5202.


# Swagger Documentation
Explore and test the API endpoints using Swagger documentation available at http://localhost:5202/swagger/index.html

# Logging
Console Logging support using Serilog

