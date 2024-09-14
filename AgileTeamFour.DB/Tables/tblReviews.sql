CREATE TABLE [tblReviews] (
    ReviewID INT PRIMARY KEY NOT NULL,
    StarsOutOf5 INT CHECK (StarsOutOf5 >= 1 AND StarsOutOf5 <= 5) NOT NULL,
    ReviewText TEXT,
    AuthorID INT NOT NULL,
    RecipientID INT NOT NULL,
    DateTime DATETIME NOT NULL
);