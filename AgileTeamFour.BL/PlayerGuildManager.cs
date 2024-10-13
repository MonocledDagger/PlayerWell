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
                                            Role = pe.Role
                                        }).ToList();

                    return playerGuilds;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

