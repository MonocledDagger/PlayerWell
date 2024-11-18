﻿CREATE TABLE [tblEvents] (
    EventID INT PRIMARY KEY NOT NULL,
    GameID INT NOT NULL,
    EventName VARCHAR(100) NOT NULL,
    Server VARCHAR(50),
    MaxPlayers INT NOT NULL,
    Type VARCHAR(50),
    Platform VARCHAR(50),
    Description TEXT,
    DateTime DATETIME NOT NULL,
    AuthorId INT NOT NULL, 
    [GuildId] INT NULL
);
