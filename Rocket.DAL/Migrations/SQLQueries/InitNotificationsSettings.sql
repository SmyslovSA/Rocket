INSERT INTO [dbo].[NotificationsSettings]
           ([Name]
           ,[NotifyIsSwitchOn]
           ,[NotifyPeriodInMinutes]
		   ,[PushUrl])
     VALUES
           ('Notifications'
           ,1
           ,60
		   ,'http://localhost:63613/api/notifications/notifyOfReleasePush');



