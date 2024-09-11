CREATE TABLE [tblComments] (
    CommentID INT PRIMARY KEY,
    EventID INT,
    TimePosted DATETIME,
    AuthorID INT,
    Text TEXT
);