using AgileTeamFour.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgileTeamFour.PL;

namespace AgileTeamFour.BL
{
    public static class PlayerEventManager
    {
        public static int Insert(int playerid, int eventid, string Role, bool rollback = false)
        {
            try
            {
                int results = 0;
                //Need to Scaffold
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayerEvent tblPlayerEvent = new tblPlayerEvent();
                    tblPlayerEvent.PlayerID = playerid;
                    tblPlayerEvent.EventID = eventid;
                    tblPlayerEvent.PlayerEventID = dc.tblPlayerEvents.Any() ? dc.tblPlayerEvents.Max(sa => sa.PlayerEventID) + 1 : 1;

                    dc.tblPlayerEvents.Add(tblPlayerEvent);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Delete(int Playerid, int eventid, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayerEvent? tblPlayerEvent = dc.tblPlayerEvents
                        .FirstOrDefault(sa => sa.PlayerID == Playerid
                        && sa.EventID == eventid);

                    if (tblPlayerEvent != null)
                    {
                        dc.tblPlayerEvents.Remove(tblPlayerEvent);
                        results = dc.SaveChanges();
                    }
                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
