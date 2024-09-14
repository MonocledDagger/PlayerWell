CREATE TABLE [tblPlayerEvents] (
    PlayerEventID INT PRIMARY KEY NOT NULL,
    PlayerID INT NOT NULL,
    EventID INT NOT NULL,
    Role VARCHAR(50)
);