using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL
{
    public class GuildManager
    {
        public static int Insert(ref int guildID,
                                string guildName,
                                string guildDescription,
                                int LeaderId,
                                bool rollback = false)
        {
            try
            {
                
                Guild guild = new Guild
                {
                    GuildId = guildID,
                    GuildName = guildName,
                    Description = guildDescription,
                    LeaderId = LeaderId,

                };

                int results = Insert(guild, rollback);



                guildID = guild.GuildId;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static int Insert(Guild guild, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGuild entity = new tblGuild();

                    //Generate GuildID
                    entity.GuildId = dc.tblGuilds.Any() ? dc.tblGuilds.Max(s => s.GuildId) + 1 : 1;

                    entity.GuildName = guild.GuildName;
                    entity.Description = guild.Description;
                    entity.LeaderId = guild.LeaderId;



                    guild.GuildId = (int)entity.GuildId;

                    dc.tblGuilds.Add(entity);
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

        public static int Update(Guild guild, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblGuild entity = dc.tblGuilds.FirstOrDefault(e => e.GuildId == guild.GuildId);

                    if (entity != null)
                    {
                        entity.GuildId = guild.GuildId;
                        entity.GuildName= guild.GuildName;
                        entity.Description = guild.Description;
                        entity.LeaderId= guild.LeaderId;

                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }



        public static int Delete(int GuildID, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblGuild row = dc.tblGuilds.FirstOrDefault(d => d.GuildId == GuildID);


                    dc.tblGuilds.Remove(row);

                    results = dc.SaveChanges();

                    if (rollback) dbContextTransaction.Rollback();

                }
                return results;

            }
            catch (Exception)
            {

                throw;
            }

        }


        public static Guild LoadByID(int id)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblGuild entity = dc.tblGuilds.FirstOrDefault(e => e.GuildId == id);

                    if (entity != null)
                    {
                        return new Guild
                        {
                            GuildId = entity.GuildId,
                            
                            GuildName = entity.GuildName,
                            
                            Description = entity.Description,
                            LeaderId = entity.LeaderId,
                            

                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Guild> Load()
        {
            try
            {
                List<Guild> list = new List<Guild>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from e in dc.tblGuilds
                     select new
                     {
                         e.GuildId,
                         
                         e.GuildName,
                         
                         e.Description,
                         e.LeaderId
                         
                     })
                     .ToList()
                     .ForEach(guild => list.Add(new Guild
                     {
                         GuildId = guild.GuildId,
                         
                         GuildName = guild.GuildName,
                         
                         Description = guild.Description,
                         LeaderId = guild.LeaderId, 
                         

                     }));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetLeaderName(int guildID)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Query the PlayerEvent table for the specified eventID
                    int authorId = GuildManager.LoadByID(guildID)?.LeaderId ?? UserManager.Load().FirstOrDefault().UserID;

                    string? authorName = UserManager.Load().FirstOrDefault(u => u.UserID == authorId)?.UserName;

                    return authorName ?? "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
