CREATE TABLE [dbo].[AppUsers] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [UserName]      VARCHAR (MAX) NOT NULL,
    [PasswordHash]  VARCHAR (MAX) NULL,
    [SecurityStamp] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

