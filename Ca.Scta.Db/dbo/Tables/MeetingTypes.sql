CREATE TABLE [dbo].[MeetingTypes] (
    [MeetingTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [ShortName]     NVARCHAR (10) NOT NULL,
    [LongName]      NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MeetingTypeId] ASC),
    UNIQUE NONCLUSTERED ([LongName] ASC),
    UNIQUE NONCLUSTERED ([ShortName] ASC)
);

