CREATE TABLE Players (
    PlayerID INT PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
    Email VARCHAR(100),
    Password VARCHAR(100) NOT NULL,
    IconPic VARCHAR(255),
    Bio TEXT
);