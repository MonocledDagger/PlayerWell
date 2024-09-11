CREATE TABLE Events (
    EventID INT PRIMARY KEY,
    GameID INT,
    EventName VARCHAR(100) NOT NULL,
    Server VARCHAR(50),
    MaxPlayers INT,
    EventType VARCHAR(50),
    Platform VARCHAR(50),
    Description TEXT,
    DateTime DATETIME,
    FOREIGN KEY (GameID) REFERENCES Games(GameID)
);
