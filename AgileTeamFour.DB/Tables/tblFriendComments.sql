CREATE TABLE [tblFriendComments] (
    ID INT PRIMARY KEY NOT NULL,
    FriendSentToID INT NOT NULL,
    TimePosted DATETIME NOT NULL,
    AuthorID INT NOT NULL,
    Text TEXT NOT NULL
);