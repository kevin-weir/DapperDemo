CREATE TABLE [dbo].[Customer] (
    [CustomerId]      BIGINT IDENTITY(1, 1) NOT NULL,
    [FirstName]       NVARCHAR (50) NOT NULL,
    [LastName]        NVARCHAR (50) NOT NULL,
    [PhoneNumber] NCHAR(10) NOT NULL, 
    [StreetAddress] NVARCHAR(100) NOT NULL, 
    [City] NVARCHAR(25) NOT NULL, 
    [CountryId] BIGINT NULL, 
    [ProvinceId] BIGINT NULL, 
    [PostalCode] NCHAR(6) NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Customer_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([CountryId]), 
    CONSTRAINT [FK_Customer_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [Province]([ProvinceId]), 
    CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId]) 
);

