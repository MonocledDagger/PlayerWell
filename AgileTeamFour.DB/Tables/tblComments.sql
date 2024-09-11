CREATE TABLE Comments (
    CommentID INT PRIMARY KEY,
    EventID INT,
    TimePosted DATETIME,
    AuthorID INT,
    AuthorName VARCHAR(50),
    Text TEXT,
    FOREIGN KEY (EventID) REFERENCES Events(EventID),
    FOREIGN KEY (AuthorID) REFERENCES Players(PlayerID)
);