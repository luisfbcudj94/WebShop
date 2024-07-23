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