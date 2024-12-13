namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utUser
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
            //Seed only runs when after website for the first time.
            //If you delete database and run test without running website,
            //this returns false because there won't be any users in the table
            Assert.AreEqual(7, UserManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            User player = new User()
            {
                UserID = UserManager.Load().Max(e => e.UserID) + 1,
                FirstName="None",
                LastName="None",
                UserName = "None",
                Email = "None",
                Password = "None",
                IconPic = "None",
                Bio = "None",
                DateOfBirth = DateTime.Now,
                AccessLevel = "Player"
            };

            int results = UserManager.Insert(player, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            User player = new User();
            player = UserManager.LoadById(1);
            tblUser entity = dc.tblUsers.Where(e => e.UserID == 1).FirstOrDefault();

            Assert.AreEqual(player.UserID, entity.UserID);
            Assert.AreEqual(player.UserName, entity.UserName);
            Assert.AreEqual(player.Email, entity.Email);
            Assert.AreEqual(player.Password, entity.Password);
            Assert.AreEqual(player.IconPic, entity.IconPic);
            Assert.AreEqual(player.Bio, entity.Bio);
            Assert.AreEqual(player.DateOfBirth, entity.DateOfBirth);
            Assert.AreEqual(player.AccessLevel, entity.AccessLevel);

        }

        [TestMethod]
        public void UpdateTest()
        {
            User player = new User()
            {
                UserID = 1,
                FirstName ="None",
                LastName ="None",
                UserName = "None",
                Email = "None",
                Password = "None",
                IconPic = "None",
                Bio = "None",
                DateOfBirth = DateTime.Now,
                AccessLevel = "Player"
            };

            int result = UserManager.Update(player, true);
            Assert.IsTrue(result > 0);
        }
    }
}