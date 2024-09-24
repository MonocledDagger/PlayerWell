namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utComment
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
            Assert.AreEqual(3, CommentManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = EventManager.Load().FirstOrDefault().EventID;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            Comment comment = new Comment()
            {
                CommentID = CommentManager.Load().Max(c => c.CommentID) + 1,
                EventID = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"

            };

            int results = CommentManager.Insert(comment, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Comment comment = new Comment();
            comment = CommentManager.LoadById(1);
            tblComment entity = dc.tblComments.Where(e => e.CommentID == 1).FirstOrDefault();

            Assert.AreEqual(comment.CommentID, entity.CommentID);
            Assert.AreEqual(comment.EventID, entity.EventID);
            Assert.AreEqual(comment.TimePosted, entity.TimePosted);
            Assert.AreEqual(comment.AuthorID, entity.AuthorID);
            Assert.AreEqual(comment.Text, entity.Text);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = EventManager.Load().FirstOrDefault().EventID;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            Comment comment = new Comment()
            {
                CommentID = 1,
                EventID = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"

            };

            int result = CommentManager.Update(comment, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = CommentManager.Delete(1, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}