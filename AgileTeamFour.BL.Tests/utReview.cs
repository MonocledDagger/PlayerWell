using static AgileTeamFour.BL.Tests.utReview;

namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utReview
    {
        protected AgileTeamFourEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            dc = new AgileTeamFourEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(42, ReviewManager.Load().Count); //test only works on intial data
        }

        [TestMethod]
        public void InsertTest()
        {
            int? id = UserManager.Load().FirstOrDefault().UserID;
            if (id == null)
                Assert.Fail("No valid UserID in Users Table");

            Review review = new Review()
            {
                ReviewID = ReviewManager.Load().Max(e => e.ReviewID) + 1,
                StarsOutOf5 = 1,
                ReviewText = "None",
                AuthorID = (int)id,
                RecipientID = (int)id,
                DateTime = DateTime.Now

            };

            int results = ReviewManager.Insert(review, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Review review = new Review();
            review = ReviewManager.LoadById(501);
            tblReview entity = dc.tblReviews.Where(e => e.ReviewID == 501).FirstOrDefault();

            Assert.AreEqual(review.ReviewID, entity.ReviewID);
            Assert.AreEqual(review.StarsOutOf5, entity.StarsOutOf5);
            Assert.AreEqual(review.ReviewText, entity.ReviewText);
            Assert.AreEqual(review.AuthorID, entity.AuthorID);
            Assert.AreEqual(review.RecipientID, entity.RecipientID);
            Assert.AreEqual(review.DateTime, entity.DateTime);

        }

        [TestMethod]
        public void UpdateTest()
        {
            int? id = UserManager.Load().FirstOrDefault().UserID;
            if (id == null)
                Assert.Fail("No valid UserID in Users Table");

            Review review = new Review()
            {
                ReviewID = 501,
                StarsOutOf5 = 1,
                ReviewText = "None",
                AuthorID = (int)id,
                RecipientID = (int)id,
                DateTime = DateTime.Now

            };

            int result = ReviewManager.Update(review, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = ReviewManager.Delete(501, true);
            Assert.AreNotEqual(result, 0);
        }
    }      
}