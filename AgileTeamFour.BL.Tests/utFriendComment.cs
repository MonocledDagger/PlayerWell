using static System.Net.Mime.MediaTypeNames;

namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utFriendComment
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
            Assert.AreEqual(15, FriendCommentManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = FriendManager.Load().FirstOrDefault().ReceiverID;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            FriendComment FriendComment = new FriendComment()
            {
                ID = FriendCommentManager.Load().Max(c => c.ID) + 1,
                FriendSentToID = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"
            };

            int results = FriendCommentManager.Insert(FriendComment, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            FriendComment FriendComment = new FriendComment();
            FriendComment = FriendCommentManager.LoadById(1);
            tblFriendComment entity = dc.tblFriendComments.Where(e => e.ID == 1).FirstOrDefault();

            Assert.AreEqual(FriendComment.ID, entity.ID);
            Assert.AreEqual(FriendComment.FriendSentToID, entity.FriendSentToID);
            Assert.AreEqual(FriendComment.TimePosted, entity.TimePosted);
            Assert.AreEqual(FriendComment.AuthorID, entity.AuthorID);
            Assert.AreEqual(FriendComment.Text, entity.Text);
        }

        [TestMethod]
        public void UpdateTest()
        {

            int? id = FriendManager.Load().FirstOrDefault().ReceiverID;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            FriendComment FriendComment = new FriendComment()
            {
                ID = 1,
                FriendSentToID = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"
            };

            int result = FriendCommentManager.Update(FriendComment, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = FriendCommentManager.Delete(1, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}