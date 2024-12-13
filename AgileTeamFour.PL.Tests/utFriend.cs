using AgileTeamFour.BL.Models;
using Microsoft.CodeAnalysis;

namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utFriend : utBase<tblFriend>
    {

        [TestMethod]
        public void LoadTest()
        {
            
            int expected = 14;
            var Friends = base.LoadTest();
            Assert.AreEqual(expected, Friends.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblFriend row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {

            
            int rowsAffected = InsertTest(new tblFriend
            {
                ID = FriendManager.Load().Max(e => e.ID) + 1,
                Status = "Pending",
                ReceiverID = 3,
                SenderID = 4,
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblFriend row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.Status = row.Status + "Test";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblFriend row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}