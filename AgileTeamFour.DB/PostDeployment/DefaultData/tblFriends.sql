BEGIN
	INSERT INTO tblFriend(ID, Status, ReceiverID, SenderID)
	VALUES
	(1,'Pending',4,1),
	(2,'Approved',2,4),
	(3,'Blocked',3,4),
	(4,'Approved',1,2),
	(5,'Approved',3,2),
	(6,'Approved',5,2),
	(7,'Approved',6,2),
	(8,'Pending',5,4),
	(9,'Blocked',4,6);
END