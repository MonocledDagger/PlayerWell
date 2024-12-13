BEGIN
	INSERT INTO tblComments (CommentID, EventID, TimePosted, AuthorID, [Text])
	VALUES
	(1, 1001,'2024-08-13 02:21:12', 1, 'Make sure to be on time for the event.'),
	(2, 1001, '2024-08-13 02:20:58', 2, 'This event is a great way to learn more about the game.'),
	(3, 1001, '2024-08-13 02:20:21', 3, 'Everyone is welcome to join this event.')
END