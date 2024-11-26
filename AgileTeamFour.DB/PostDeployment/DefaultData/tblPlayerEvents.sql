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
    (20, 5, 1003, 'Blue Team'),

    (100, 1, 1033, 'Viewer'),
    (200, 2, 1033, 'Viewer'),
    (300, 3, 1033, 'Viewer'),
    (400, 4, 1033, 'Viewer'),
    (500, 5, 1033, 'Viewer'),
    (600, 6, 1033, 'Viewer'),
    (700, 7, 1033, 'Viewer'),

    (800, 1, 1030, 'Player'),
    (900, 2, 1030, 'Player'),
    (1000, 3, 1030, 'Player'),
    (1100, 4, 1030, 'Player'),
    (1200, 5, 1030, 'Player'),
    (1300, 6, 1030, 'Player'),
    (1400, 7, 1030, 'Player'),

    (1500, 1, 1040, 'Participant'),
    (1600, 2, 1040, 'Participant'),
    (1700, 3, 1040, 'Participant'),
    (1800, 4, 1040, 'Participant'),
    (1900, 5, 1040, 'Participant'),
    (2000, 6, 1040, 'Participant'),
    (2100, 7, 1040, 'Participant');
END
