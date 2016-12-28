CREATE TABLE [dbo].[MeetingTimes] (
    [MeetingTimeId] INT      IDENTITY (1, 1) NOT NULL,
    [MeetingId]     INT      NULL,
    [MeetingDay]    INT      NOT NULL,
    [TimeOfDay]     TIME (0) NULL,
    PRIMARY KEY CLUSTERED ([MeetingTimeId] ASC),
    CONSTRAINT [fk_MeetingTimes_Meetings] FOREIGN KEY ([MeetingId]) REFERENCES [dbo].[Meetings] ([MeetingId])
);

