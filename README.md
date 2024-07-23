# WebShop

# Database Schema Overview

This section provides an overview of the database schema created for the WebShop project. It includes details about each table, its columns, data types, and best practices used in the database design.

## Tables and Their Fields

### 1. Categories
- **Description**: Stores information about product categories.
- **Fields**:
  - `CategoryId` (UNIQUEIDENTIFIER, PRIMARY KEY, DEFAULT NEWID()): Unique identifier for each category.
  - `CategoryName` (NVARCHAR(100), NOT NULL): Name of the category.
  - `Description` (NVARCHAR(255)): Description of the category.

- **Indexes**:
  - `IDX_CategoryId` on `CategoryId` to optimize lookups.

### 2. Products
- **Description**: Stores information about products available in the shop.
- **Fields**:
  - `ProductId` (UNIQUEIDENTIFIER, PRIMARY KEY, DEFAULT NEWID()): Unique identifier for each product.
  - `ProductName` (NVARCHAR(100), NOT NULL): Name of the product.
  - `ProductCode` (NVARCHAR(50), NOT NULL): Unique code for the product.
  - `Description` (NVARCHAR(255)): Description of the product.
  - `Price` (DECIMAL(18, 2), NOT NULL): Price of the product.
  - `StockQuantity` (INT, NOT NULL): Quantity of the product in stock.
  - `ImageBase64` (NVARCHAR(MAX)): Base64-encoded image of the product.

- **Indexes**:
  - `IDX_ProductId` on `ProductId` to improve performance.

### 3. ProductCategories
- **Description**: Manages the many-to-many relationship between products and categories.
- **Fields**:
  - `ProductId` (UNIQUEIDENTIFIER, FOREIGN KEY): References `ProductId` in the `Products` table.
  - `CategoryId` (UNIQUEIDENTIFIER, FOREIGN KEY): References `CategoryId` in the `Categories` table.

- **Primary Key**: Composite key (`ProductId`, `CategoryId`).

### 4. Customers
- **Description**: Stores customer information.
- **Fields**:
  - `CustomerId` (UNIQUEIDENTIFIER, PRIMARY KEY, DEFAULT NEWID()): Unique identifier for each customer.
  - `FirstName` (NVARCHAR(100), NOT NULL): Customer's first name.
  - `LastName` (NVARCHAR(100), NOT NULL): Customer's last name.
  - `Email` (NVARCHAR(255), NOT NULL): Customer's email address.

### 5. Orders
- **Description**: Stores order information for customers.
- **Fields**:
  - `OrderId` (UNIQUEIDENTIFIER, PRIMARY KEY, DEFAULT NEWID()): Unique identifier for each order.
  - `CustomerId` (UNIQUEIDENTIFIER, NOT NULL, FOREIGN KEY): References `CustomerId` in the `Customers` table.
  - `OrderDate` (DATETIME, NOT NULL): Date when the order was placed.

- **Indexes**:
  - `IDX_CustomerId` on `CustomerId` to improve performance.

### 6. OrderProducts
- **Description**: Manages the many-to-many relationship between orders and products.
- **Fields**:
  - `OrderProductId` (UNIQUEIDENTIFIER, PRIMARY KEY, DEFAULT NEWID()): Unique identifier for each order-product relation.
  - `OrderId` (UNIQUEIDENTIFIER, FOREIGN KEY): References `OrderId` in the `Orders` table.
  - `ProductId` (UNIQUEIDENTIFIER, FOREIGN KEY): References `ProductId` in the `Products` table.
  - `Quantity` (INT, NOT NULL): Quantity of the product ordered.

### Best Practices Used

1. **Normalization**: The database schema is designed to be in Third Normal Form (3NF) to reduce redundancy and ensure data integrity.
2. **Use of GUIDs**: `UNIQUEIDENTIFIER` (GUID) is used for primary keys to ensure globally unique values and facilitate distributed data generation.
3. **Indexes**: Indexes are created on primary keys and foreign keys to improve query performance.
4. **Referential Integrity**: Foreign keys are used to maintain referential integrity between related tables.
5. **Data Types**: Appropriate data types are used for each column to optimize storage and performance (e.g., `DECIMAL` for prices, `NVARCHAR` for text).
6. **Base64 Image Storage**: Product images are stored as base64 strings in `ImageBase64` for ease of retrieval and integration with front-end applications.

This schema is designed to provide a solid foundation for a web shop application, ensuring efficient data management and retrieval.

# API Overview

The WebShop API was developed to provide robust and efficient endpoints for managing products, categories, customers, and orders. Below are the key practices and architecture used in creating the API.

## Architecture and Practices

### 1. **Layered Architecture**
- **Presentation Layer**: Contains the controllers that handle HTTP requests and responses.
- **Application Layer**: Contains services that implement business logic.
- **Core Layer**: Contains domain entities and interfaces.
- **Infrastructure Layer**: Contains the implementation of repositories and database context.

### 2. **Dependency Injection**
- Utilized to manage dependencies and promote loose coupling. Services and repositories are injected into controllers and other services.

### 3. **DTOs (Data Transfer Objects)**
- Used to transfer data between layers. DTOs are used in service methods to encapsulate the data sent to and received from the API.

### 4. **Asynchronous Programming**
- All data access methods are implemented asynchronously using `async` and `await` to improve scalability and performance.

### 5. **Entity Framework Core**
- Used as the ORM (Object-Relational Mapper) for database operations. It simplifies data access by allowing us to work with C# objects instead of SQL queries.

### 6. **Automapper**
- Used for object-to-object mapping, simplifying the conversion between domain models and DTOs.

### 7. **Validation**
- Data annotations and FluentValidation are used to validate incoming data to ensure data integrity and consistency.

## Key Endpoints

### 1. Catalog
- `GET /api/Catalog/products`: Retrieves a paginated list of products.
- `GET /api/Catalog/products/{id}`: Retrieves a product by its ID.


### 2. Orders
- `GET /api/orders`: Retrieves a list of orders.
- `GET /api/orders/{id}`: Retrieves an order by its ID.
- `POST /api/orders/{orderId}/products/{productId}/addtocart`: Adds a product to an existing or new order's cart for a specific customer.

### 3. Shopping Cart
- `POST /api/shoppingcart/update`: Updates the cart with new items or changes to existing items.
- `POST /api/shoppingcart/checkout`: Processes the current cart and completes the order.

# Frontend Development

The frontend of the WebShop project has been developed using React with Redux to manage the application's global state. Below is a description of the component structure, services used to connect to the API, and key practices employed in frontend development.

## Project Structure

### 1. **Components**
The main components of the frontend are organized in the `components` folder. The two main components are:

#### 1.1. **Cart.tsx**
- **Description**: This component displays the user's shopping cart. It allows viewing the added products, updating the quantity of each product, and removing products. It also provides functionality to place the order.
- **Main Functions**:
  - Load and display cart items from local storage.
  - Update product quantities and remove products from the cart.
  - Process the order and communicate with the backend to create a new order.
  - Display success and error modals in case of issues during the ordering process.

#### 1.2. **ProductList.tsx**
- **Description**: This component displays a list of available products in the store. It allows users to view product details and add products to the cart.
- **Main Functions**:
  - Retrieve and display the list of products from the API.
  - Allow users to add products to the cart.

### 2. **Services**
Services have been created to interact with the API and handle HTTP requests. These services are located in the `services` folder and are designed to simplify communication with the backend.

#### 2.1. **orderService.ts**
- **Description**: This service handles requests related to orders. It includes methods to add products to the cart and process orders.
- **Main Methods**:
  - `addToCart(orderId: string, orderData: OrderData)`: Sends order data to the backend to create or update an order.

#### 2.2. **productService.ts**
- **Description**: This service handles requests related to products. It includes methods to get the list of products and details of a specific product.
- **Main Methods**:
  - `getProducts()`: Retrieves a list of products.
  - `getProductById(productId: string)`: Retrieves details of a specific product.

## Best Practices

1. **Componentization**: Components are designed to be reusable and are kept separate in different files for better organization and maintainability.
2. **Abstracted Services**: Services abstract the API communication logic, allowing components to focus solely on UI presentation and specific user interface logic.
3. **State Management with Redux**: Redux is used to manage the global state of the shopping cart, ensuring consistent and predictable state updates across the application.
4. **Modularity and Scalability**: The project structure is designed to be modular and scalable, making it easier to add new features and components as the project grows.

This approach ensures a smooth and efficient user experience while keeping the code organized and easy to maintain.

# Running the Application

To get the WebShop application up and running, follow these steps:

## 1. Execute Database Scripts

1. **Run the `CreateDB.sql` Script**
   - This script creates the necessary database schema.
   - Use SQL Server Management Studio or any other SQL client to execute the script against your SQL Server instance.

2. **Run the `DataSeed.sql` Script**
   - This script seeds the database with initial data.
   - Execute it after running `CreateDB.sql` to populate the database with sample data.

## 2. Configure Database Connection

1. **Set Up Your Connection String**
   - Open the `appsettings.json` file in the API project.
   - Configure the connection string to point to your SQL Server database. Update the `ConnectionStrings` section with your database server details.

   Example:
   ```json
   "SqlConnectionString": {
    "default": "Server=LUPEREZ;Database=WebShopDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }