CREATE TABLE [dbo].[AppUsers] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [UserName]      VARCHAR (100) NOT NULL,
    [PasswordHash]  VARCHAR (MAX) NULL,
    [SecurityStamp] VARCHAR (MAX) NULL,
    [Email] VARCHAR(100) NULL,
	[EmailConfirmed] bit NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

