-- Create the WebShopDb database
CREATE DATABASE WebShopDb;
GO

-- Switch to the WebShopDb database
USE WebShopDb;
GO

-- Create Categories table
CREATE TABLE Categories (
    CategoryId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CategoryName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);
GO

-- Create index on CategoryId
CREATE INDEX IDX_CategoryId ON Categories(CategoryId);
GO

-- Create Products table
CREATE TABLE Products (
    ProductId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    ProductName NVARCHAR(100) NOT NULL,
    ProductCode NVARCHAR(50) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(18, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    ImageBase64 NVARCHAR(MAX)
);
GO

-- Create index on ProductId
CREATE INDEX IDX_ProductId ON Products(ProductId);
GO

-- Create ProductCategories table
CREATE TABLE ProductCategories (
    ProductId UNIQUEIDENTIFIER,
    CategoryId UNIQUEIDENTIFIER,
    PRIMARY KEY (ProductId, CategoryId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
GO

-- Create Customers table
CREATE TABLE Customers (
    CustomerId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL
);
GO

-- Create Orders table
CREATE TABLE Orders (
    OrderId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    OrderDate DATETIME NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
GO

-- Create index on CustomerId
CREATE INDEX IDX_CustomerId ON Orders(CustomerId);
GO

-- Create OrderProducts table
CREATE TABLE OrderProducts (
    OrderProductId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    OrderId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,
    Quantity INT NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
GO

-- Create index on CategoryName
CREATE INDEX IDX_CategoryName ON Categories(CategoryName);
GO
