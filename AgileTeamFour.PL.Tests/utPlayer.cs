namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utPlayer : utBase<tblPlayer>
    {

        [TestMethod]
        public void LoadTest()
        {
            //Assert.AreEqual(3, dc.tblPlayers.Count());
            int expected = 3;
            var Players = base.LoadTest();
            Assert.AreEqual(expected, Players.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblPlayer row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = InsertTest(new tblPlayer
            {
                PlayerID = PlayerManager.Load().Max(e => e.PlayerID) + 1,
                UserName = "None",
                Email = "None",
                Password = "None",
                IconPic = "None",
                Bio = "None",
                DateTime = DateTime.Now
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblPlayer row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.Bio = row.Bio + "None";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblPlayer row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }

    }
}