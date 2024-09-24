namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utComment : utBase<tblComment>
    {

        [TestMethod]
        public void LoadTest()
        {
            //Assert.AreEqual(3, dc.tblComments.Count());
            int expected = 3;
            var Comments = base.LoadTest();
            Assert.AreEqual(expected, Comments.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblComment row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = EventManager.Load().FirstOrDefault().EventID;
            if(id == null)
                Assert.Fail("No valid EventID in Events Table");
            int? id2 = UserManager.Load().FirstOrDefault().UserID;
            if (id2 == null)
                Assert.Fail("No valid UserID in Users Table");

            int rowsAffected = InsertTest(new tblComment
            {
                CommentID = CommentManager.Load().Max(c => c.CommentID) + 1,
                EventID = (int)id,
                TimePosted = DateTime.Now,
                AuthorID = (int)id2,
                Text = "Test"
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblComment row = base.LoadTest().FirstOrDefault();
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
            tblComment row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}