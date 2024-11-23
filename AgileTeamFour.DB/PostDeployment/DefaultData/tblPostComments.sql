BEGIN
	INSERT INTO tblPostComments(CommentID, PostID, AuthorID,[Text], TimePosted,ParentCommentID)
	VALUES
	(1,101, 2, 'At what time are you available?', '2024-08-13 02:21:12',1),
	(2,101, 1, '10:am works for me!', '2024-08-13 02:21:12',2),
	(3,101, 2, '10:am works for me too, see you then!', '2024-08-13 02:21:12',3)
END


