BEGIN
	INSERT INTO tblPostComments(CommentID, PostID, AuthorID,[Text], TimePosted,ParentCommentID)
	VALUES
	(1,103, 2, 'At what time are you available?', '2024-08-13 02:21:12',0),
	(2,103, 1, '10:am works for me!', '2024-08-13 02:21:12',1)
	
END


