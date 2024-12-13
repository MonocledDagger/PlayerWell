BEGIN
	INSERT INTO tblFriendComments (ID, FriendSentToID, TimePosted, AuthorID, Text)
	VALUES
	(1, 1,'2024-08-13 02:21:12', 4, 'Hello, How are you?'),
	(2, 4,'2024-08-13 02:21:13', 1, 'I am doing great.  How are you?'),

	(3, 2,'2024-08-13 02:21:12', 1, 'How are things going?'),
	(4, 1,'2024-08-13 02:21:13', 2, 'Great.'),
	(5, 2,'2024-08-13 02:21:14', 1, 'That is good to hear.'),

	(6, 3,'2024-08-13 02:21:13', 1, 'Hi.  Do you think you can make it to the event?'),
	(7, 1,'2024-08-13 02:21:14', 3, 'Yes, I will be there and on time.'),

	(8, 4,'2024-08-14 02:21:14', 1, 'Good work on the event.'),
	(9, 1,'2024-08-14 02:21:15', 4, 'Thank you.  I think our team did well.'),


	(10, 3,'2024-08-11 02:21:13', 2, 'Hi.  Are you planning on going to the event?'),
	(11, 2,'2024-08-11 02:21:14', 3, 'Yes, I will be there.'),

	(12, 4,'2024-08-14 02:21:14', 2, 'Hello, how are you?'),
	(13, 2,'2024-08-14 02:21:15', 4, 'I am doing well.'),

	(14, 3,'2024-08-13 02:21:12', 4, 'Hello, How are you?'),
	(15, 4,'2024-08-13 02:21:13', 3, 'I am doing good.');

END
