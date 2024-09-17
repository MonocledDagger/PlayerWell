namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utPlayer
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
        public void LoadTest()
        {
            Assert.AreEqual(3, PlayerManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Player player = new Player()
            {
                PlayerID = PlayerManager.Load().Max(e => e.PlayerID) + 1,
                UserName = "None",
                Email = "None",
                Password = "None",
                IconPic = "None",
                Bio = "None",
                DateTime = DateTime.Now
            };

            int results = PlayerManager.Insert(player, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Player player = new Player();
            player = PlayerManager.LoadById(1);
            tblPlayer entity = dc.tblPlayers.Where(e => e.PlayerID == 1).FirstOrDefault();

            Assert.AreEqual(player.PlayerID, entity.PlayerID);
            Assert.AreEqual(player.UserName, entity.UserName);
            Assert.AreEqual(player.Email, entity.Email);
            Assert.AreEqual(player.Password, entity.Password);
            Assert.AreEqual(player.IconPic, entity.IconPic);
            Assert.AreEqual(player.Bio, entity.Bio);
            Assert.AreEqual(player.DateTime, entity.DateTime);

        }

        [TestMethod]
        public void UpdateTest()
        {
            Player player = new Player()
            {
                PlayerID = 1,
                UserName = "None",
                Email = "None",
                Password = "None",
                IconPic = "None",
                Bio = "None",
                DateTime = DateTime.Now
            };

            int result = PlayerManager.Update(player, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = PlayerManager.Delete(1, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}