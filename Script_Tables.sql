IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MiniInventoryManagementSystem')
BEGIN
    CREATE DATABASE MiniInventoryManagementSystem;
END;


use MiniInventoryManagementSystem;

IF OBJECT_ID(N'dbo.Tb_Person', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tb_Person (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(100) NOT NULL,
        FederalId VARCHAR(11) NOT NULL,
        Email VARCHAR(100) NOT NULL,
        CreateAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END;



IF OBJECT_ID(N'dbo.Tb_BranchOffice', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tb_BranchOffice (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(100) NOT NULL,
        CreateAt DATETIME NOT NULL DEFAULT GETDATE()
    );
END;


IF OBJECT_ID(N'dbo.Tb_Order', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tb_Order (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Number INT,
        Total DECIMAL(10,2),
        IsCancelled BIT NOT NULL DEFAULT(0),
        CostumerId INT NOT NULL,
        BranchOfficeId INT NOT NULL,
        CreateAt DATETIME NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME,
        CONSTRAINT Tb_Order_CostumerId FOREIGN KEY (CostumerId) REFERENCES Tb_Person(Id),
        CONSTRAINT Tb_Order_BranchOfficeId FOREIGN KEY (BranchOfficeId) REFERENCES Tb_BranchOffice(Id),
    );
END;

IF OBJECT_ID(N'dbo.Tb_OrderItem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Tb_OrderItem (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ProductCode VARCHAR(10),
        ProductName VARCHAR(100),
        Quantity INT,
        Discount DECIMAL(10,2),
        UnitPrice DECIMAL(10,2),
        Total DECIMAL(10,2),
        IsCancelled BIT NOT NULL DEFAULT(0),
        OrderId INT NOT NULL,
        CreateAt DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT Tb_OrderItem_OrderId FOREIGN KEY (OrderId) REFERENCES Tb_Order(Id),
    );
END;