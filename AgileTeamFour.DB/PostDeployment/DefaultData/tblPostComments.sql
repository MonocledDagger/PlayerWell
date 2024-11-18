BEGIN
	INSERT INTO tblPostComments(CommentID, PostID, AuthorID,[Text], TimePosted,ParentCommentID)
	VALUES
	(1,1, 2, 'At what time are you available?', '2024-08-13 02:21:12',101),
	(2,2, 1, '10:am works for me!', '2024-08-13 02:21:12',1),
	(3,3, 2, '10:am works for me too, see you then!', '2024-08-13 02:21:12',2)
END


