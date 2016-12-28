CREATE TABLE [dbo].[Areas] (
    [AreaId]   INT          IDENTITY (1, 1) NOT NULL,
    [AreaName] VARCHAR (50) NOT NULL,
    [Hotline]  CHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([AreaId] ASC)
);

