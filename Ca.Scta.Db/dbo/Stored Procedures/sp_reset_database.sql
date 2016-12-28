CREATE PROCEDURE [dbo].[sp_reset_database]
AS
	BEGIN
		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;

		DELETE MeetingTimesMeetingTypes 
		DELETE MeetingTypes
		DELETE Areas
		DELETE [Locations]
		DELETE Contacts
		DELETE Meetings
		DELETE MeetingTimes
		DBCC CHECKIDENT('[MeetingTypes]',RESEED,0)
		DBCC CHECKIDENT('[Areas]',RESEED,0)
		DBCC CHECKIDENT('[MeetingLocations]',RESEED,0)
		DBCC CHECKIDENT('[Contacts]',RESEED,0)
		DBCC CHECKIDENT('[Meetings]',RESEED,0)
		DBCC CHECKIDENT('[MeetingTimes]',RESEED,0)
	END