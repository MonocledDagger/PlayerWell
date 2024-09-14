CREATE TABLE [tblComments] (
    CommentID INT PRIMARY KEY NOT NULL,
    EventID INT NOT NULL,
    TimePosted DATETIME NOT NULL,
    AuthorID INT NOT NULL,
    Text TEXT NOT NULL
);