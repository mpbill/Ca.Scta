CREATE TABLE [dbo].[Contacts] (
    [ContactId]   INT           IDENTITY (1, 1) NOT NULL,
    [City]        VARCHAR (50)  NULL,
    [Email]       VARCHAR (254) NULL,
    [Name]        VARCHAR (50)  NULL,
    [Description] VARCHAR (50)  NULL,
    [Position]    VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([ContactId] ASC)
);

