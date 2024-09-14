CREATE TABLE [tblPlayers] (
    PlayerID INT PRIMARY KEY NOT NULL,
    UserName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    IconPic VARCHAR(255),
    Bio TEXT,
    DateTime DATETIME
);