BEGIN
	INSERT INTO tblFriend(ID, Status, ReceiverID, SenderID)
	VALUES
	(1,'Pending',4,1),
	(2,'Approved',2,4),
	(3,'Blocked',4,3);
END