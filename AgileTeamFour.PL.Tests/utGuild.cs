using AgileTeamFour.BL.Models;
using Microsoft.CodeAnalysis;

namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utGuild : utBase<tblGuild>
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
            tblGuild row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {         
            int rowsAffected = InsertTest(new tblGuild
            {
                GuildId = GuildManager.Load().Max(e => e.GuildId) + 1,
                GuildName = "None",
                LeaderId = 1,
                Description = "None",
                
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGuild row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.Description = row.Description + "None";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblGuild row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}