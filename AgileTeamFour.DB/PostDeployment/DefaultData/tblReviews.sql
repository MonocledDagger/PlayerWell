BEGIN
    INSERT INTO tblReviews (ReviewID, StarsOutOf5, ReviewText, AuthorID, RecipientID, DateTime)
    VALUES
    (501, 5, 'Player is friendly and learning the Game.', 1, 5, '2024-08-15 19:00:00'),
    (502, 5, 'I learned some things from this player.', 2, 5, '2024-08-13 11:42:12'),
    (503, 5, 'This player contributes to the group.', 3, 5, '2024-07-16 16:30:00'),
    (504, 5, 'This player likes to negotiate.', 4, 5, '2024-07-18 09:45:00'),
    (505, 1, 'Disruptive behavior', 6, 5, '2024-07-20 11:00:00'),
    (506, 3, 'Decent but could be better', 7, 5, '2024-07-21 13:15:00'),

    (507, 5, 'Our group had fun and learned a lot.', 2, 1, '2024-08-14 03:00:00'),
    (508, 5, 'Good team player', 3, 1, '2024-07-15 14:00:00'),
    (509, 5, 'Exceptional gameplay!', 4, 1, '2024-07-17 12:20:00'),    
    (601, 5, 'Awesome player, highly recommend', 5, 1, '2024-07-19 20:10:00'),
    (602, 4, 'Great strategies', 6, 1, '2024-07-22 15:40:00'),
    (603, 4, 'Great performance', 7, 1, '2024-07-24 18:00:00'),


    (604, 5, 'Solved some problems our group was having.', 1, 2, '2024-07-25 21:30:00'),
    (605, 5, 'Solid player', 3, 2, '2024-07-26 19:15:00'),
    (606, 5, 'Top-notch skills', 4, 2, '2024-07-27 22:00:00'),
    (607, 5, 'Fantastic coordination', 5, 2, '2024-07-28 08:45:00'),
    (608, 4, 'Thinks things out.', 6, 2, '2024-07-29 14:55:00'),
    (609, 4, 'Works smarter, not harder.', 7, 2, '2024-07-30 17:30:00'),


    (610, 5, 'Great Communicator and problem-solver.', 1, 3, '2024-07-31 20:05:00'),
    (611, 5, 'Good teamwork', 2, 3, '2024-08-01 12:45:00'),
    (612, 5, 'Knows his strategies.', 4, 3, '2024-08-02 13:20:00'),
    (613, 5, 'Expert level play', 5, 3, '2024-08-03 15:50:00'),
    (614, 4, 'Versatile Player', 6, 3, '2024-08-04 18:30:00'),
    (615, 4, 'Strategic thinker', 7, 3, '2024-08-05 14:25:00'),


    (616, 5, 'Thinks things through.', 1, 4, '2024-08-06 11:10:00'),
    (617, 5, 'Excellent leadership', 2, 4, '2024-08-07 16:35:00'),
    (618, 5, 'Gives good advice.', 3, 4, '2024-08-08 20:00:00'),
    (619, 5, 'Prepared well for our game play.', 5, 4, '2024-08-09 18:45:00'),
    (620, 4, 'Accomplished what the group needed.', 6, 4, '2024-08-10 21:30:00'),
    (621, 4, 'Consistent player', 7, 4, '2024-08-11 12:55:00'),


    (622, 5, 'Made it on time.', 1, 6, '2024-08-12 10:15:00'),
    (623, 5, 'Amazing reflexes', 2, 6, '2024-08-13 11:50:00'),
    (624, 5, 'Very punctual.', 3, 6, '2024-08-14 09:40:00'),
    (625, 5, 'Strong strategic thinking', 4, 6, '2024-08-15 14:10:00'),
    (626, 1, 'Needs to control their temper', 5, 6, '2024-08-16 16:45:00'),
    (627, 2, 'Could Communicate beter.', 7, 6, '2024-08-17 19:30:00'),


    (628, 5, 'Worked with the group.', 1, 7, '2024-08-18 17:00:00'),
    (629, 5, 'Followed group strategy.', 2, 7, '2024-08-19 20:30:00'),
    (630, 5, 'Was on time for game play.', 3, 7, '2024-08-20 15:15:00'),
    (631, 5, 'Very cooperative', 4, 7, '2024-08-21 18:00:00'),
    (632, 1, 'Needs to be more engaged', 5, 7, '2024-08-22 11:30:00'),
    (633, 2, 'Couldn’t keep up', 6, 7, '2024-08-23 09:00:00');   
  
END
