using AgileTeamFour.BL.Models;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utFriend
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
            Assert.AreEqual(14, FriendManager.Load().Count); //Test only works with initial data
        }

        [TestMethod]
        public void InsertTest()
        {
            Friend Friend1 = new Friend()
            {
                ID = FriendManager.Load().Max(e => e.ID) + 1,
                Status= "Pending",
                SenderID = 7,
                ReceiverID = 6,                
            };

            int results = FriendManager.Insert(Friend1, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Friend Friend1 = new Friend();
            Friend1 = FriendManager.LoadByID(1);
            tblFriend entity = dc.tblFriends.Where(e => e.ID == 1).FirstOrDefault();

            Assert.AreEqual(Friend1.ID, entity.ID);
            Assert.AreEqual(Friend1.Status, entity.Status);
            Assert.AreEqual(Friend1.ReceiverID, entity.ReceiverID);
            Assert.AreEqual(Friend1.SenderID, entity.SenderID);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = GameManager.Load().FirstOrDefault().GameID;
            if (id == null)
                Assert.Fail("No valid GameID in Friends Table");

            Friend Friend1 = new Friend()
            {
                ID = 1,
                Status = "Accepted",
                SenderID = 2,
                ReceiverID = 3,

            };

            int result = FriendManager.Update(Friend1, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = FriendManager.Delete(1, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}