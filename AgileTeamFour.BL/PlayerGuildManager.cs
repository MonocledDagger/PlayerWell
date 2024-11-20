using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL
{
    public class PlayerGuildManager
    {
        public static int Insert(int playerid, int guildid, string Role, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayerGuild tblPlayerGuild = new tblPlayerGuild();
                    tblPlayerGuild.PlayerID = playerid;
                    tblPlayerGuild.GuildID = guildid;
                    tblPlayerGuild.Role = Role;
                    tblPlayerGuild.JoinDate = DateTime.Now;

                    tblPlayerGuild.PlayerGuildID = dc.tblPlayerGuilds.Any() ? dc.tblPlayerGuilds.Max(sa => sa.PlayerGuildID) + 1 : 1;

                    dc.tblPlayerGuilds.Add(tblPlayerGuild);
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

        public static int Delete(int playerid, int guildid, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayerGuild? tblPlayerGuild = dc.tblPlayerGuilds
                        .FirstOrDefault(sa => sa.PlayerID == playerid
                        && sa.GuildID == guildid);

                    if (tblPlayerGuild != null)
                    {
                        dc.tblPlayerGuilds.Remove(tblPlayerGuild);
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

        // Added  method to load player guilds by GuildID
        public static List<PlayerGuild> LoadByGuildID(int guildID)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Query the PlayerGuild table for the specified guildID
                    var playerGuilds = (from pe in dc.tblPlayerGuilds
                                        join u in dc.tblUsers on pe.PlayerID equals u.UserID
                                        where pe.GuildID == guildID
                                        select new PlayerGuild
                                        {
                                            PlayerGuildID = pe.PlayerGuildID,
                                            GuildID = pe.GuildID,
                                            PlayerID = u.UserID,
                                            JoinDate= (DateTime)pe.JoinDate,
                                            Role = pe.Role,
                                            UserName = u.UserName
                                        }).ToList();

                    return playerGuilds;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static string InviteGuild(string playerName, int guildID)
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
                    tblPlayerGuild newGuild = new tblPlayerGuild
                    {
                        GuildID = guildID,
                        PlayerID = player.UserID,
                        Role = "Guest", // Represents a pending invite without needing a new table
                        JoinDate= DateTime.Now,
                    };

                    dc.tblPlayerGuilds.Add(newGuild);
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

