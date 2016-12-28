CREATE TABLE [dbo].[Meetings] (
    [MeetingId]         INT          IDENTITY (1, 1) NOT NULL,
    [MeetingLocationId] INT          NULL,
    [AreaId]            INT          NULL,
    [MeetingName]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MeetingId] ASC),
    CONSTRAINT [fk_Meetings_Areas] FOREIGN KEY ([AreaId]) REFERENCES [dbo].[Areas] ([AreaId]),
    CONSTRAINT [fk_Meetings_MeetingLocations] FOREIGN KEY ([MeetingLocationId]) REFERENCES [dbo].[Locations] ([LocationId])
);

