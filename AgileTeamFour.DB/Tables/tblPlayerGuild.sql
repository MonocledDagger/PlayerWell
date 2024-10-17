CREATE TABLE [dbo].[tblPlayerGuild]
(
	[PlayerGuildID] INT NOT NULL PRIMARY KEY, 
    [PlayerID] INT NOT NULL, 
    [GuildID] INT NOT NULL, 
    [Role] VARCHAR(10) NULL, 
    [JoinDate] DATETIME NULL
)
