CREATE TABLE [tblReviews] (
    ReviewID INT PRIMARY KEY,
    StarsOutOf5 INT CHECK (StarsOutOf5 >= 1 AND StarsOutOf5 <= 5),
    ReviewText TEXT,
    AuthorID INT,
    RecipientID INT,
    DateTime DATETIME
);