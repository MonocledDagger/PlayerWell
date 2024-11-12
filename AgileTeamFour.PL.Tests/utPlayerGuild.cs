namespace AgileTeamFour.PL.Tests
{ 
    [TestClass]
    public class utPlayerGuild : utBase<tblPlayerGuild>
    {


        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = PlayerGuildManager.Insert(1, 1, "None", true);
 

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int rowsAffected = PlayerGuildManager.Delete(4, 1, true);
            Assert.IsTrue(rowsAffected == 1);          
        }

    }
}