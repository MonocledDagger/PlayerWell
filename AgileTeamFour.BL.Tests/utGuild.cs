using AgileTeamFour.BL.Models;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.BL.Tests
{
    [TestClass]
    public class utGuild
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
            Assert.AreEqual(3, GuildManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest()
        {
            

            Guild guild = new Guild()
            {
                GuildId = GuildManager.Load().Max(e => e.GuildId) + 1,
                
                GuildName = "None",
                
                Description = "None",
                LeaderId = 1

            };

            int results = GuildManager.Insert(guild, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void LoadByIDTest()
        {
            Guild guild = new Guild();
            guild = GuildManager.LoadByID(1);
            tblGuild entity = dc.tblGuilds.Where(e => e.GuildId == 1).FirstOrDefault();

            Assert.AreEqual(guild.GuildId, entity.GuildId);
            
            Assert.AreEqual(guild.GuildName, entity.GuildName);
            
            Assert.AreEqual(guild.Description, entity.Description);
            Assert.AreEqual(guild.LeaderId, entity.LeaderId);
        }

        [TestMethod]
        public void UpdateTest()
        {
            
            
            Guild guild = new Guild()
            {
                GuildId = 1,
                GuildName = "Testaroo",
                LeaderId = 1,
                Description = "None",
                

            };

            int result = GuildManager.Update(guild, true);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int result = GuildManager.Delete(1, true);
            Assert.AreNotEqual(result, 0);
        }
    }
}