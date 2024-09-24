using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgileTeamFour.BL
{
    public static class PlayerEventManager
    {
        public static int Insert(int playerid, int eventid, string Role, bool rollback = false)
        {
            try
            {
                int results = 0;
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

        public static int Delete(int playerid, int eventid, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayerEvent? tblPlayerEvent = dc.tblPlayerEvents
                        .FirstOrDefault(sa => sa.PlayerID == playerid
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

        // Added  method to load player events by EventID
        public static List<PlayerEvent> LoadByEventID(int eventID)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Query the PlayerEvent table for the specified eventID
                    var playerEvents = (from pe in dc.tblPlayerEvents
                                        join u in dc.tblUsers on pe.PlayerID equals u.UserID
                                        where pe.EventID == eventID
                                        select new PlayerEvent
                                        {
                                            PlayerEventID = pe.PlayerEventID,
                                            EventID = pe.EventID,
                                            PlayerID = u.UserID,
                                            UserName = u.UserName, 
                                            Role = pe.Role 
                                        }).ToList();

                    return playerEvents;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
