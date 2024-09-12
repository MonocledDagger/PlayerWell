BEGIN
    INSERT INTO tblReviews (ReviewID, StarsOutOf5, ReviewText, AuthorID, RecipientID, DateTime)
    VALUES
    (501, 5, 'Played well and communicated OK', 1, 2,'2024-08-15 19:00:00' ),
    (502, 1, 'Very bad mouth!', 1, 3, '2024-08-15 19:00:00'),
    (503, 5, 'Friend of a friend was a great play mate', 2, 1, '2024-08-14 03:00:00'),
    (504, 1, 'Spammed the chat terrible things', 2, 3, '2024-08-13 11:42:12');    
END