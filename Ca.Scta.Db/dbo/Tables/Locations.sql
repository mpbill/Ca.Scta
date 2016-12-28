CREATE TABLE [dbo].[Locations] (
    [LocationId]          INT            IDENTITY (1, 1) NOT NULL,
    [Address1]            VARCHAR (50)   NOT NULL,
    [Address2]            VARCHAR (50)   NULL,
    [Address3]            VARCHAR (50)   NULL,
    [City]                VARCHAR (50)   NOT NULL,
    [State]               CHAR (2)       NOT NULL,
    [Zip]                 CHAR (5)       NOT NULL,
    [MapLink]             VARCHAR (2000) NULL,
    [LocationName]        VARCHAR (50)   NULL,
    [LocationDescription] VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([LocationId] ASC)
);

