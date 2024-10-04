BEGIN
    INSERT INTO tblPlayers (PlayerID, UserName, Email, Password, IconPic, Bio, DateTime)
    VALUES
    (1, 'George', 'george@gmail.com', 'random123!', '.\pathtoimage.png', 'This is a short description of me and the games I enjoy playing with others.', '2024-08-12 19:00:00'),
    (2, 'Jerry', 'jerry@gmail.com', 'jerrysecure!2024', '.\path_to_jerry_image.png', 'Avid gamer and technology enthusiast. Love multiplayer games and strategy!', '2024-08-12 19:00:00'),
    (3, 'Rachel', 'rachel@gmail.com', 'rachelrocks!2024', '.\path_to_rachel_image.png', 'Passionate about gaming and storytelling. Always up for a good co-op game!', '2024-08-12 19:00:00'),
    (4, 'Bill', 'bill@gmail.com', 'bill', '.\path_to_rachel_image.png', 'Passionate about gaming and storytelling. Always up for a good co-op game!', '2024-08-12 19:00:00'),
    (5, 'Test', 'test@gmail.com', 'test', '.\path_to_rachel_image.png', 'Passionate about gaming and storytelling. Always up for a good co-op game!', '2024-08-12 19:00:00');
END