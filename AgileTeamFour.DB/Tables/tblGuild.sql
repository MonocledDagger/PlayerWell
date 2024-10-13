CREATE TABLE [dbo].[tblGuild]
(
	[GuildId] INT NOT NULL PRIMARY KEY, 
    [GuildName] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(200) NULL, 
    [LeaderId] INT NOT NULL
)
