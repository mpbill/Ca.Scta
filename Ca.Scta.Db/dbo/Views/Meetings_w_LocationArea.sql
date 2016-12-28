CREATE VIEW [dbo].[Meetings_w_LocationArea]
	AS 
	SELECT 
	Meetings.MeetingId,MeetingName,Meetings.AreaId,AreaName,Meetings.MeetingLocationId,[LocationName],[LocationDescription],Address1,COUNT(MeetingTimes.MeetingTimeId) AS MeetingTimeCount
	FROM [Meetings]
	LEFT JOIN [Areas] ON Meetings.AreaId=Areas.AreaId
	LEFT JOIN [Locations] ON Meetings.MeetingLocationId=[Locations].[LocationId]
	LEFT JOIN MeetingTimes ON Meetings.MeetingId=MeetingTimes.MeetingId
	GROUP BY Meetings.MeetingId,MeetingName,Meetings.AreaId,AreaName,Meetings.MeetingLocationId,[LocationName],[LocationDescription],Address1