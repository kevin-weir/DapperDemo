CREATE TABLE [dbo].[Country]
(
	[CountryId] BIGINT IDENTITY(1, 1) NOT NULL,
    [CountryAbbreviation] NCHAR(3) NOT NULL, 
    [CountryName] NCHAR(30) NOT NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [AK_Country_CountryAbbreviation] UNIQUE ([CountryAbbreviation]), 
    CONSTRAINT [PK_Country] PRIMARY KEY ([CountryId]) 
)

GO
