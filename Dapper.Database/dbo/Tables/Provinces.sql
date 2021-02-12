CREATE TABLE [dbo].[Provinces]
(
	[ProvinceId] INT IDENTITY(1, 1) NOT NULL,
    [CountryId] INT NOT NULL, 
    [ProvinceAbbreviation] NVARCHAR(3) NOT NULL, 
    [ProvinceName] NVARCHAR(25) NOT NULL, 
    [CreatedBy] NVARCHAR(256) NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(256) NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Provinces_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries]([CountryId]), 
    CONSTRAINT [AK_Provinces_ProvinceAbbreviation] UNIQUE ([ProvinceAbbreviation]), 
    CONSTRAINT [PK_Provinces] PRIMARY KEY ([ProvinceId])
)
