BEGIN
	INSERT INTO tblFriendComments (ID, FriendSentToID, TimePosted, AuthorID, Text)
	VALUES
	(1, 2,'2024-08-13 02:21:12', 4, 'Hello, How are you?'),
	(2, 4, '2024-08-13 02:22:58', 2, 'I am doing great.  It is nice to hear from you'),
	(3, 2,'2024-08-13 02:21:12', 4, 'Good to hear.');
END
