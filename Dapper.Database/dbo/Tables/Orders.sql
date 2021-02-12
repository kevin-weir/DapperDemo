CREATE TABLE [dbo].[Orders]
(
	[OrderId] INT NOT NULL IDENTITY(1, 1) , 
    [CustomerId] INT NOT NULL, 
    [OrderNumber] NVARCHAR(10) NOT NULL, 
    [OrderDate] DATETIME NOT NULL, 
    [OrderTotal] DECIMAL(18, 2) NOT NULL, 
    [OrderTaxes] DECIMAL(18, 2) NOT NULL, 
    [CreatedBy] NVARCHAR(256) NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(256) NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]), 
    CONSTRAINT [FK_Orders_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [Customers]([CustomerId]) ON DELETE CASCADE 
)
