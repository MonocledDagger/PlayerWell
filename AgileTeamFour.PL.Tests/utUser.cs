namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utUser : utBase<tblUser>
    {
        [TestMethod]
        public void LoadTest()
        {

            int expected = 7;
            var users = base.LoadTest();
            Assert.AreEqual(expected, users.Count());

        }

        [TestMethod]
        public void LoadByIDTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();
            Assert.IsNotNull(row);
        }


        [TestMethod]
        public void InsertTest()
        {
            int rowsAffected = InsertTest(new tblUser
            {
                UserID = UserManager.Load().Max(e => e.UserID) + 1,
                FirstName = "None",
                LastName = "None",
                UserName = "None",
                Email = "None",
                Password = "None",
                IconPic = "None",
                Bio = "None",
                DateOfBirth = DateTime.Now,
                AccessLevel = "Player"
            });

            Assert.AreEqual(1, rowsAffected);
        }

        [TestMethod]
        public void UpdateTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                row.AccessLevel = "Test";
                int rowsAffected = UpdateTest(row);
                Assert.AreEqual(1, rowsAffected);
            }
        }

        [TestMethod]
        public void DeleteTest()
        {
            tblUser row = base.LoadTest().FirstOrDefault();
            if (row != null)
            {
                int rowsAffected = DeleteTest(row);
                Assert.IsTrue(rowsAffected == 1);
            }
        }
    }
}
