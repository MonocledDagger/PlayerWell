namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utPlayerEvent
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
        public void InsertTest()
        {
            int results = PlayerEventManager.Insert(1, 1, "None", true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = PlayerEventManager.Delete(1, 1001, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}