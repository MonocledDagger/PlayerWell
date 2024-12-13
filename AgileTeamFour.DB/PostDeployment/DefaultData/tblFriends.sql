BEGIN
	INSERT INTO tblFriend(ID, Status, ReceiverID, SenderID)
	VALUES
	(1,'Approved',1,2),
	(2,'Approved',1,3),
	(3,'Approved',1,4),	
	(4,'Approved',2,3),
	(5,'Approved',2,4),	
	(6,'Approved',3,4),

	(7,'Pending',1,5),
	(8,'Pending',2,5),
	(9,'Pending',3,5),
	(10,'Pending',4,5),

	(11,'Blocked',1,6),
	(12,'Blocked',2,6),
	(13,'Blocked',3,6),
	(14,'Blocked',4,6);

END