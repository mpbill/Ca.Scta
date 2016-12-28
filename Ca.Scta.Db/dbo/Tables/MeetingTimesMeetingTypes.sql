CREATE TABLE [dbo].[MeetingTimesMeetingTypes] (
    [MeetingTypeId] INT NOT NULL,
    [MeetingTimeId] INT NOT NULL,
    CONSTRAINT [PK_MeetingsMeetingTypesId] PRIMARY KEY CLUSTERED ([MeetingTypeId] ASC, [MeetingTimeId] ASC),
    CONSTRAINT [FK_MeetingTimeId] FOREIGN KEY ([MeetingTimeId]) REFERENCES [dbo].[MeetingTimes] ([MeetingTimeId]),
    CONSTRAINT [FK_MeetingTypeId] FOREIGN KEY ([MeetingTypeId]) REFERENCES [dbo].[MeetingTypes] ([MeetingTypeId])
);

