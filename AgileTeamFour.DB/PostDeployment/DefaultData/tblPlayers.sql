BEGIN
    INSERT INTO tblPlayers (PlayerID, UserName, Email, Password, IconPic, Bio)
    VALUES
    (1, 'George', 'george@gmail.com', 'random123!', '.\pathtoimage.png', 'This is a short description of me and the games I enjoy playing with others.'),
    (2, 'Jerry', 'jerry@gmail.com', 'jerrysecure!2024', '.\path_to_jerry_image.png', 'Avid gamer and technology enthusiast. Love multiplayer games and strategy!'),
    (3, 'Rachel', 'rachel@gmail.com', 'rachelrocks!2024', '.\path_to_rachel_image.png', 'Passionate about gaming and storytelling. Always up for a good co-op game!');
END