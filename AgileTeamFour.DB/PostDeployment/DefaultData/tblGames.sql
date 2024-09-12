BEGIN
	INSERT INTO tblGames (GameID, GameName, Platform, Description, Picture, Genre)
	VALUES
	(66, 'World of Warcraft', 'PC', 'An open world game with endless quests for power and might!', '.\wow.png','MMORPG'),
	(59, 'Roblox', 'PC', 'A fun game to build and spend time with friends,','.\roblox.png','Social'),
	(45, 'Fallout: The Boardgame', 'PC', 'A table top role playing game.', '.\falloutboardgame.png','Tabletop');
END