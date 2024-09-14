CREATE TABLE [tblGames] (
    GameID INT PRIMARY KEY NOT NULL,
    GameName VARCHAR(100) NOT NULL,
    Platform VARCHAR(50),
    Description TEXT NOT NULL,
    Picture VARCHAR(255),
    Genre VARCHAR(50)
);