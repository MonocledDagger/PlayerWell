namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utReview : utBase<tblReview>
    {

        [TestMethod]
        public void LoadTest()
        {
            //Assert.AreEqual(3, dc.tblReviews.Count());
            int expected = 42;
            var Reviews = base.LoadTest();
            Assert.AreEqual(expected, Reviews.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblReview row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {
            int? id = UserManager.Load().FirstOrDefault().UserID;
            if (id == null)
                Assert.Fail("No valid UserID in Users Table");

            int rowsAffected = InsertTest(new tblReview
            {
                ReviewID = ReviewManager.Load().Max(e => e.ReviewID) + 1,
                StarsOutOf5 = 1,
                ReviewText = "None",
                AuthorID = (int)id,
                RecipientID = (int)id,
                DateTime = DateTime.Now
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblReview row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.ReviewText = row.ReviewText + "None";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblReview row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}