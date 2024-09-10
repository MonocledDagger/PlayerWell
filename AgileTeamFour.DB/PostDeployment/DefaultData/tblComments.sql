BEGIN
	INSERT INTO tblComment (CommentID, EventID, TimePosted, AuthorID, AuthorName, Text)
	VALUES
	(1, 1001,'2024-08-13 02:21:12', 1001,'Extra Field From Players', 'I cannot wait to try the new expansion!'),
	(2, 1001, '2024-08-13 02:20:58', 1002,'Extra Field From Players', 'Missing this would be a shame please make it everyone!'),
	(3, 1001, '2024-08-13 02:20:21', 1003,'Extra Field From Players', 'I heard their will be gold drops at the gates lets get there early')
END