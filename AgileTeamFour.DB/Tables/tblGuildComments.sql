CREATE TABLE [tblGuildComments] (
    CommentID INT PRIMARY KEY NOT NULL,
    [GuildId] INT NOT NULL,
    TimePosted DATETIME NOT NULL,
    AuthorID INT NOT NULL,
    Text TEXT NOT NULL
);
