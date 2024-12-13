using AgileTeamFour.BL.Models;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utEvent
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
        public void LoadTest() //Test only works with initial data
        {
            Assert.AreEqual(31, EventManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = GameManager.Load().FirstOrDefault().GameID;
            if (id == null)
                Assert.Fail("No valid GameID in Events Table");

            Events event1 = new Events()
            {
                EventID = EventManager.Load().Max(e => e.EventID) + 1,
                GameID = (int)id,
                EventName = "None",
                Server = "None",
                MaxPlayers = 3,
                Type = "None",
                Platform = "None",
                Description = "None",
                DateTime = DateTime.Now

            };

            int results = EventManager.Insert(event1, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Events event1 = new Events();
            event1 = EventManager.LoadByID(1001);
            tblEvent entity = dc.tblEvents.Where(e => e.EventID == 1001).FirstOrDefault();

            Assert.AreEqual(event1.EventID, entity.EventID);
            Assert.AreEqual(event1.GameID, entity.GameID);
            Assert.AreEqual(event1.EventName, entity.EventName);
            Assert.AreEqual(event1.Server, entity.Server);
            Assert.AreEqual(event1.MaxPlayers, entity.MaxPlayers);
            Assert.AreEqual(event1.Type, entity.Type);
            Assert.AreEqual(event1.Platform, entity.Platform);
            Assert.AreEqual(event1.Description, entity.Description);
            Assert.AreEqual(event1.DateTime, entity.DateTime);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = GameManager.Load().FirstOrDefault().GameID;
            if (id == null)
                Assert.Fail("No valid GameID in Events Table");

            Events event1 = new Events()
            {
                EventID = 1001,
                GameID = (int)id,
                EventName = "None",
                Server = "None",
                MaxPlayers = 3,
                Type = "None",
                Platform = "None",
                Description = "None",
                DateTime = DateTime.Now

            };

            int result = EventManager.Update(event1, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = EventManager.Delete(1001, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}