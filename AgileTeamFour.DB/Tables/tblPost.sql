
CREATE TABLE [tblPost] (
    PostID INT PRIMARY KEY NOT NULL,
    TimePosted DATETIME NOT NULL,
    AuthorID INT NOT NULL,
    [Image] varchar(255) NOT NULL,
    [Text] TEXT NOT NULL
);