CREATE VIEW [dbo].[MeetingTypes_w_MeetingTimeCount]
	AS SELECT MeetingTypes.MeetingTypeId,ShortName,LongName,COUNT(MeetingTimesMeetingTypes.MeetingTimeId) as MeetingTimeCount FROM MeetingTypes
	LEFT JOIN MeetingTimesMeetingTypes ON MeetingTypes.MeetingTypeId=MeetingTimesMeetingTypes.MeetingTypeId
	GROUP BY MeetingTypes.MeetingTypeId,ShortName,LongName;