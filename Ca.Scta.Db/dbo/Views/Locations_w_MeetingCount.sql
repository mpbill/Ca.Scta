CREATE VIEW [dbo].[Locations_w_MeetingCount]
	AS SELECT [Locations].[LocationId],Address1,Address2,Address3,City,[State],Zip,[LocationName],[LocationDescription],COUNT(Meetings.MeetingId) AS MeetingCount
	FROM [Locations]
	LEFT JOIN Meetings ON [Locations].[LocationId]=Meetings.MeetingLocationId
	GROUP BY 
	[Locations].[LocationId],Address1,Address2,Address3,City,[State],Zip,[LocationName],[LocationDescription]