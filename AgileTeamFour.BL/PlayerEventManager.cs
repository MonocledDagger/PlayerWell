using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
                    tblPlayerEvent.Role = Role;
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

        public static string InviteEvent(string playerName, int eventID)
        {
            string message;
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Find the UserID that matches the given playerName in the PlayerTable
                    var player = (from u in dc.tblUsers
                                  where u.UserName == playerName
                                  select new { u.UserID }).FirstOrDefault();

                    // Check if the player exists
                    if (player == null)
                        return message = "Player not found.";

                    // Create a new entry in tblPlayerEvents
                    tblPlayerEvent newEvent = new tblPlayerEvent
                    {
                        EventID = eventID,
                        PlayerID = player.UserID,
                        Role = "Guest" // Represents a pending invite without needing a new table
                    };

                    dc.tblPlayerEvents.Add(newEvent);
                    dc.SaveChanges();  // Save changes to the database

                    return message = "Player invited successfully"; // Success
                }
            }
            catch (Exception ex)
            {
                // Set error message if an exception occurs
                return message = ex.Message;
            }
        }
    }
}
