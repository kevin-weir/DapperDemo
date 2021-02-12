CREATE TABLE [dbo].[Customers] (
    [CustomerId]      INT IDENTITY(1, 1) NOT NULL,
    [FirstName]       NVARCHAR (50) NOT NULL,
    [LastName]        NVARCHAR (50) NOT NULL,
    [PhoneNumber] NVARCHAR(10) NOT NULL, 
    [StreetAddress] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(25) NOT NULL, 
    [CountryId] INT NULL, 
    [ProvinceId] INT NULL, 
    [PostalCode] NVARCHAR(7) NOT NULL, 
    [CreatedBy] NVARCHAR(256) NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(256) NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Customers_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries]([CountryId]), 
    CONSTRAINT [FK_Customers_Provinces] FOREIGN KEY ([ProvinceId]) REFERENCES [Provinces]([ProvinceId]), 
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId]) 
);

