CREATE TABLE [dbo].[Province]
(
	[ProvinceId] INT IDENTITY(1, 1) NOT NULL,
    [CountryId] INT NOT NULL, 
    [ProvinceAbbreviation] NVARCHAR(3) NOT NULL, 
    [ProvinceName] NVARCHAR(25) NOT NULL, 
    [CreatedBy] NVARCHAR(256) NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(256) NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Province_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([CountryId]), 
    CONSTRAINT [AK_Province_ProvinceAbbreviation] UNIQUE ([ProvinceAbbreviation]), 
    CONSTRAINT [PK_Province] PRIMARY KEY ([ProvinceId])
)
