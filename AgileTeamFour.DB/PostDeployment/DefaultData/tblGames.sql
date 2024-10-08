BEGIN
	INSERT INTO tblGames (GameID, GameName, Platform, Description, Picture, Genre)
	VALUES
	(23599, 'World of Warcraft', 'PC', 'An open world game with endless quests for power and might!', 'https://media.rawg.io/media/games/0d9/0d930ea604ee240c5af30c58f73ddf48.jpg','MMORPG'),
	(779, 'Roblox', 'PC', 'A fun game to build and spend time with friends,','https://media.rawg.io/media/games/3af/3af386b6e26be6741b711ae6215ef42f.jpg','Social'),
	(3070, 'Fallout 4', 'Xbox', 'A interactive shooter role playing game.', 'https://media.rawg.io/media/games/d82/d82990b9c67ba0d2d09d4e6fa88885a7.jpg','RPG');
END