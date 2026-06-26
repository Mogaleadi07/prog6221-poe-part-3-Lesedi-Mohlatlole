CREATE TABLE [dbo].[TaskHelper]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(250) NOT NULL, 
    [Description] NVARCHAR(500) NOT NULL, 
    [ReminderDate] DATETIME NULL, 
    [IsCompleted] BIT NOT NULL DEFAULT 0
)
