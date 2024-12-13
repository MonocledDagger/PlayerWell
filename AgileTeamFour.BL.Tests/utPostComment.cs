using AgileTeamFour.BL.Models;
using static System.Net.Mime.MediaTypeNames;

namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utPostComment
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
        public void LoadTest() //Post comment manager loads the number of top posts, not replies postcomments
        {
            Assert.AreEqual(1, PostCommentManager.Load().Count); 
        }

        [TestMethod]
        public void InsertTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = PostManager.Load().FirstOrDefault().PostID;
            if (id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            PostComment PostComment = new PostComment()
            {
                CommentID = 0, //Manager in Insert statement will automatically assign CommentID
                PostID = (int)id,
                AuthorID = (int)id2,
                Text = "Test",
                TimePosted = DateTime.Now,
                ParentCommentID = 0
            };

            int results = PostCommentManager.Insert(PostComment, true);
            Assert.AreEqual(1, results);
        }


        /* LoadById method in manager loads the first result based on PostID, not PostCommentID.
        [TestMethod]
        public void LoadByIDTest()
        {
            PostComment PostComment = new PostComment();
            PostComment = PostCommentManager.LoadById(1);
            tblPostComment entity = dc.tblPostComments.Where(e => e.CommentID == 1).FirstOrDefault();

            Assert.AreEqual(PostComment.CommentID, entity.CommentID);
            Assert.AreEqual(PostComment.PostID, entity.PostID);
            Assert.AreEqual(PostComment.AuthorID, entity.AuthorID);
            Assert.AreEqual(PostComment.Text, entity.Text);
            Assert.AreEqual(PostComment.TimePosted, entity.TimePosted);
            Assert.AreEqual(PostComment.ParentCommentID, entity.ParentCommentID);
        }
        */
    }
}