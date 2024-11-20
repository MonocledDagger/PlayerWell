CREATE TABLE [dbo].[tblPostComments]
(
	CommentID INT NOT NULL PRIMARY KEY, 
    [PostID] INT NOT NULL, 
    [AuthorID] INT NOT NULL, 
    [Text] TEXT NOT NULL, 
    [TimePosted] DATETIME NOT NULL, 
    [ParentCommentID] INT NOT NULL
)
