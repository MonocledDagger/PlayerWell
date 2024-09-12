CREATE TABLE [tblEvents] (
    EventID INT PRIMARY KEY,
    GameID INT,
    EventName VARCHAR(100) NOT NULL,
    Server VARCHAR(50),
    MaxPlayers INT,
    Type VARCHAR(50),
    Platform VARCHAR(50),
    Description TEXT,
    DateTime DATETIME
);
