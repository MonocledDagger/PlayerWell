CREATE TABLE [dbo].[tblFriend]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Status] VARCHAR(50) NOT NULL, 
    [SenderID] INT NOT NULL, 
    [ReceiverID] INT NOT NULL
)
