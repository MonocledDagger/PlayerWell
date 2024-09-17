namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utGame
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
            Assert.AreEqual(3, GameManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            Game game = new Game()
            {
                GameID = GameManager.Load().Max(e => e.GameID) + 1,
                GameName = "None",
                Platform = "None",
                Description = "None",
                Picture = "None",
                Genre = "None"
            };

            int results = GameManager.Insert(game, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Game game = new Game();
            game = GameManager.LoadByID(66);
            tblGame entity = dc.tblGames.Where(e => e.GameID == 66).FirstOrDefault();

            Assert.AreEqual(game.GameID, entity.GameID);
            Assert.AreEqual(game.GameName, entity.GameName);
            Assert.AreEqual(game.Platform, entity.Platform);
            Assert.AreEqual(game.Description, entity.Description);
            Assert.AreEqual(game.Picture, entity.Picture);
            Assert.AreEqual(game.Genre, entity.Genre);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Game game = new Game()
            {
                GameID = 66,
                GameName = "None",
                Platform = "None",
                Description = "None",
                Picture = "None",
                Genre = "None"
            };

            int result = GameManager.Update(game, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = GameManager.Delete(66, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}