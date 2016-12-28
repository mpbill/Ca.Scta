CREATE VIEW MeetingDetails
	AS SELECT 
	Meetings.MeetingId,
	MeetingName,
	[Locations].[LocationId],
	[Locations].Address1,
	[Locations].Address2,
	[Locations].Address3,
	[Locations].City,
	[Locations].[State],
	[Locations].Zip,
	[Locations].MapLink,
	[Locations].[LocationName],
	[Locations].[LocationDescription],
	Areas.AreaId,
	Areas.AreaName,
	Areas.Hotline,
	MeetingTimes.MeetingTimeId,
	MeetingTimes.MeetingDay,
	MeetingTimes.TimeOfDay,
	MeetingTypes.MeetingTypeId,
	MeetingTypes.LongName,
	MeetingTypes.ShortName	
	FROM Meetings
	LEFT JOIN [Locations] ON Meetings.MeetingLocationId=[Locations].[LocationId]
	LEFT JOIN Areas ON Meetings.AreaId=Areas.AreaId
	LEFT JOIN MeetingTImes ON Meetings.MeetingId=MeetingTimes.MeetingId
	LEFT JOIN MeetingTimesMeetingTypes ON MeetingTimes.MeetingTimeId=MeetingTimesMeetingTypes.MeetingTimeId
	LEFT JOIN MeetingTypes ON MeetingTimesMeetingTypes.MeetingTypeId=MeetingTypes.MeetingTypeId