namespace AgileTeamFour.PL.Tests
{ 
    [TestClass]
    public class utPlayerEvent : utBase<tblPlayerEvent>
    {
        [TestMethod]
        public void InsertTest()
        {
           int rowsAffected = InsertTest(new tblPlayerEvent
            {
                PlayerID = 1,
                EventID = 1,
                Role = "None"
            });

            Assert.AreEqual(1, rowsAffected);
        }
        [TestMethod]
        public void DeleteTest()
        {
            tblPlayerEvent row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}