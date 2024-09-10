BEGIN
	INSERT INTO tblGame (GameID, GameName, Platform, Description, Picture, Genre)
	VALUES
	(51, 'World of Warcraft', 'PC', 'An open world game with endless quests for power and might!', '.\wow.png','MMORPG'),
	(52, 'Roblox', 'PC', 'A fun game to build and spend time with friends,','.\roblox.png','Social'),
	(53, 'Fallout: The Boardgame', 'PC', 'A table top role playing game.', '.\falloutboardgame.png','Tabletop');
END