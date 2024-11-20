BEGIN
	INSERT INTO tblFriendComments (ID, FriendSentToID, TimePosted, AuthorID, Text)
	VALUES
	(1, 2,'2024-08-13 02:21:12', 4, 'Hello, How are you?'),
	(2, 4, '2024-08-13 02:22:58', 2, 'I am doing great.  It is nice to hear from you'),
	(3, 2,'2024-08-12 02:21:12', 4, 'Good to hear.'),
	(4, 2,'2024-08-11 02:21:13', 1, 'Great.'),
	(5, 2,'2024-08-14 02:21:14', 3, 'Good work.'),
	(6, 2,'2024-08-15 02:21:15', 4, 'Glad to see you.'),
	(7, 2,'2024-08-16 02:21:16', 5, 'Wishing you the best.'),
	(8, 2,'2024-08-17 02:21:17', 1, 'What is a good game to play?'),
	(9, 2,'2024-08-18 02:21:18', 3, 'How about we join an event?'),
	(10, 1,'2024-08-10 02:21:19', 2, 'Great day today!'),
	(11, 3,'2024-08-11 02:21:20', 2, 'What is the plan?'),
	(12, 4,'2024-08-12 02:21:21', 2, 'How many people are in your guild?'),
	(13, 5,'2024-08-13 02:21:22', 2, 'What event should we join?'),
	(14, 1,'2024-08-14 02:21:23', 2, 'What is a good time?'),
	(15, 3,'2024-08-15 02:21:24', 2, 'What is a good day?');
END
