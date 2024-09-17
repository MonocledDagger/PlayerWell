namespace AgileTeamFour.BL.Tests
{ 
    [TestClass]
    public class utPlayerEvent : utBase<tblPlayerEvent>
    {


        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = PlayerEventManager.Insert(1, 1, "None", true);
 

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int rowsAffected = PlayerEventManager.Delete(1, 1001, true);
            Assert.IsTrue(rowsAffected == 1);          
        }

    }
}