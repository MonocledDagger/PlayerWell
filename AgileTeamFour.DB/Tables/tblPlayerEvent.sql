CREATE TABLE PlayerEvent (
    PlayerEventID INT PRIMARY KEY,
    PlayerID INT,
    EventID INT,
    Role VARCHAR(50),
    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID),
    FOREIGN KEY (EventID) REFERENCES Events(EventID)
);