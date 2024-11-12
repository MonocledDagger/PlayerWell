namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utGuildComment
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
            Assert.AreEqual(3, GuildCommentManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = GuildManager.Load().FirstOrDefault().GuildId;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            GuildComment GuildComment = new GuildComment()
            {
                CommentID = GuildCommentManager.Load().Max(c => c.CommentID) + 1,
                GuildId = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"

            };

            int results = GuildCommentManager.Insert(GuildComment, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            GuildComment GuildComment = new GuildComment();
            GuildComment = GuildCommentManager.LoadById(1);
            tblGuildComment entity = dc.tblGuildComments.Where(e => e.CommentID == 1).FirstOrDefault();

            Assert.AreEqual(GuildComment.CommentID, entity.CommentID);
            Assert.AreEqual(GuildComment.GuildId, entity.GuildId);
            Assert.AreEqual(GuildComment.TimePosted, entity.TimePosted);
            Assert.AreEqual(GuildComment.AuthorID, entity.AuthorID);
            Assert.AreEqual(GuildComment.Text, entity.Text);
        }

        [TestMethod]
        public void UpdateTest()
        {
            
            int? id = GuildManager.Load().FirstOrDefault().GuildId;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            GuildComment GuildComment = new GuildComment()
            {
                CommentID = 1,
                GuildId = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"

            };

            int result = GuildCommentManager.Update(GuildComment, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = GuildCommentManager.Delete(1, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}