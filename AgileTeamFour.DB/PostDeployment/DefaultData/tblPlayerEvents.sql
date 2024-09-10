BEGIN
    INSERT INTO tblPlayerEvent (PlayerEventID, PlayerID, EventID, Role)
    VALUES
    (10, 1001,'Tank'),
    (10, 1002, 'Rouge'),
    (10, 1003, 'Healer'),
    (20, 1001, null),
    (20, 1002, null),
    (20, 1003, null),
    (30, 1001, 'Red Team'),
    (30, 1002, 'Red Team'),
    (30, 1003, 'Blue Team');

END