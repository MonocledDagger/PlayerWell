BEGIN
    INSERT INTO tblPlayerEvents (PlayerEventID, PlayerID, EventID, Role)
    VALUES
    (10, 1, 1001, 'Tank'),
    (11, 2, 1001, 'Rouge'),
    (12, 3, 1001, 'Healer'),
    (13, 1, 1002, ''),
    (14, 2, 1002, ''),
    (15, 3, 1002, ''),
    (16, 1, 1003, 'Red Team'),
    (17, 2, 1003, 'Red Team'),
    (18, 3, 1003, 'Blue Team'),
    (19, 4, 1003, 'Red Team'),
    (20, 5, 1003, 'Blue Team');

END