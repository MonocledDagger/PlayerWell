BEGIN
	INSERT INTO tblEvents (EventID, GameID, EventName, Server, MaxPlayers, Type, Platform, Description, DateTime)
	VALUES
	(1001, 66, 'WoW Expansion Party', 'US East', 32, 'Raid', 'PC', 'Raid for the first day of the new Wow Expansion', '2024-08-12 19:00:00'),
	(1002, 59, 'Weekly Social Meetup', '', '', 'Social', 'All', 'Meetup for the Current Game Members', '2024-11-11 04:30:00'),
	(1003, 45, 'Fallout: The Boardgame', '', 4, 'Role Play', 'PC', 'A Fallout themed board game I need 3 more people to play with!', '2024-09-23 11:45:00');
END