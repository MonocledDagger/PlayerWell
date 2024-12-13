namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utFriendComment : utBase<tblFriendComment>
    {

        [TestMethod]
        public void LoadTest()
        {
            int expected = 15;
            var Comments = base.LoadTest();
            Assert.AreEqual(expected, Comments.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblFriendComment row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
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

            int rowsAffected = InsertTest(new tblFriendComment
            {
                ID = FriendCommentManager.Load().Max(c => c.ID) + 1,
                FriendSentToID = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblFriendComment row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.Text = row.Text + "Test";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblFriendComment row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}
