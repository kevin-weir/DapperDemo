﻿CREATE TABLE [dbo].[Countries]
(
	[CountryId] INT IDENTITY(1, 1) NOT NULL,
    [CountryAbbreviation] NVARCHAR(3) NOT NULL, 
    [CountryName] NVARCHAR(30) NOT NULL, 
    [CreatedBy] NVARCHAR(256) NULL, 
    [CreatedDateTime] DATETIME NOT NULL, 
    [ModifiedBy] NVARCHAR(256) NULL, 
    [ModifiedDateTime] DATETIME NOT NULL, 
    CONSTRAINT [AK_Countries_CountryAbbreviation] UNIQUE ([CountryAbbreviation]), 
    CONSTRAINT [PK_Countries] PRIMARY KEY ([CountryId]) 
)

GO
