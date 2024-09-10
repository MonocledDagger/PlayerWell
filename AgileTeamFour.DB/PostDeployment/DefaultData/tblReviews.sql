BEGIN
    INSERT INTO tblReview (ReviewID, StartOutOf5, ReviewText, AuthorID, RecipientID)
    VALUES
    (501, 5, 'Played well and communicated OK', 1001, 1002),
    (502, 0, 'Very bad mouth!', 1001, 1003),
    (503, 5, 'Friend of a friend was a great play mate', 1002, 1001),
    (504, 0, 'Spammed the chat terrible things', 1002, 1003);    
END