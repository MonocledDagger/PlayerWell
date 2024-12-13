using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utPostComment : utBase<tblPostComment>
    {

        [TestMethod]
        public void LoadTest() //Unlike the BL Manager, the PL load test gets all rows, including replies
        {
            int expected = 2;
            var Comments = base.LoadTest();
            Assert.AreEqual(expected, Comments.Count());

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

            int max = 0;
            List<PostComment> Comments = PostCommentManager.Load();
            foreach (var comment in Comments)
            {
                int temp = comment.Replies.Max(c => c.CommentID);
                if(temp > max) 
                    max = temp;
            }


            int rowsAffected = InsertTest(new tblPostComment
            {
                CommentID = max + 1,
                PostID = (int)id,
                AuthorID = (int)id2,
                Text = "Test",
                TimePosted = DateTime.Now,
                ParentCommentID = 0
            });

            Assert.AreEqual(1, rowsAffected);
        }
        
    }
}