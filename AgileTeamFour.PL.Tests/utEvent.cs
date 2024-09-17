using AgileTeamFour.BL.Models;
using Microsoft.CodeAnalysis;

namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utEvent : utBase<tblEvent>
    {

        [TestMethod]
        public void LoadTest()
        {
            //Assert.AreEqual(3, dc.tblEvents.Count());
            int expected = 3;
            var Events = base.LoadTest();
            Assert.AreEqual(expected, Events.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblEvent row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {

            //If program uses foreign keys, it is necessary to have foreign keys that match to primary keys in corresponding tables, in order to insert the values.
            int? id = GameManager.Load().FirstOrDefault().GameID;
            if (id == null)
                Assert.Fail("No valid GameID in Events Table");
            int rowsAffected = InsertTest(new tblEvent
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
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblEvent row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.Type = row.Type + "None";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblEvent row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}