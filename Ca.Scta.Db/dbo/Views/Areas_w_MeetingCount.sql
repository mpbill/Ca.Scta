CREATE VIEW [dbo].[Areas_w_MeetingCount]
	AS SELECT Areas.AreaId,AreaName,Hotline,COUNT(Meetings.MeetingId) AS MeetingCount FROM [Areas]
	LEFT JOIN Meetings ON Areas.AreaId=Meetings.AreaId
	GROUP BY Areas.AreaId,AreaName,Hotline