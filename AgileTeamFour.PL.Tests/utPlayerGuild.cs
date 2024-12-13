namespace AgileTeamFour.PL.Tests
{ 
    [TestClass]
    public class utPlayerGuild : utBase<tblPlayerGuild>
    {
        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = InsertTest(new tblPlayerGuild
            {
                PlayerID = 1,
                GuildID = 1,
                Role = "None"
            });

            Assert.AreEqual(1, rowsAffected);
        }
        [TestMethod]
        public void DeleteTest()
        {
            tblPlayerGuild row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}