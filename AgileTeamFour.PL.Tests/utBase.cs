using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AgileTeamFour.PL.Tests
{
    [TestClass]
    public class utBase<T> where T : class
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

        public virtual List<T> LoadTest()
        {
            return dc.Set<T>().ToList();

        }
        public int InsertTest(T row)
        {
            dc.Set<T>().Add(row);
            return dc.SaveChanges();
        }
        public int UpdateTest(T row)
        {
            dc.Entry(row).State = EntityState.Modified;
            return dc.SaveChanges();
        }
        public int DeleteTest(T row)
        {
            dc.Set<T>().Remove(row);
            return dc.SaveChanges();
        }


    }
}