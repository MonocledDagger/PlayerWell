CREATE TABLE Games (
    GameID INT PRIMARY KEY,
    GameName VARCHAR(100) NOT NULL,
    Platform VARCHAR(50),
    Description TEXT,
    Picture VARCHAR(255),
    Genre VARCHAR(50)
);