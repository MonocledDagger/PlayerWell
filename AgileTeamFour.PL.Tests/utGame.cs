namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utGame : utBase<tblGame>
    {

        [TestMethod]
        public void LoadTest()
        {
            //Assert.AreEqual(3, dc.tblGames.Count());
            int expected = 3;
            var Games = base.LoadTest();
            Assert.AreEqual(expected, Games.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = InsertTest(new tblGame
            {
                GameID = GameManager.Load().Max(e => e.GameID) + 1,
                GameName = "None",
                Platform = "None",
                Description = "None",
                Picture = "None",
                Genre = "None"
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblGame row = base.LoadTest().FirstOrDefault();
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
            tblGame row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}