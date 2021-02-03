CREATE TABLE [dbo].[Province]
(
	[ProvinceId] BIGINT IDENTITY(1, 1) NOT NULL,
    [CountryId] BIGINT NOT NULL, 
    [ProvinceAbbreviation] NVARCHAR(3) NOT NULL, 
    [ProvinceName] NCHAR(25) NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_Province_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([CountryId]), 
    CONSTRAINT [AK_Province_ProvinceAbbreviation] UNIQUE ([ProvinceAbbreviation]), 
    CONSTRAINT [PK_Province] PRIMARY KEY ([ProvinceId])
)
