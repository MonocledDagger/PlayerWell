
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace AgileTeamFour.BL
{
    public static class GuildCommentManager
    {

        //Adding a value to any foreign key requires the foreign key value first exist as a a primary key value of the referenced table
        //Comments.AuthorID uses Players.PlayerID as a foreign key
        //Comments.EventID uses Event.EventID as a foreign key
        public static int Insert(GuildComment comment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGuildComment entity = new tblGuildComment();
                    entity.CommentID = dc.tblGuildComments.Any() ? dc.tblGuildComments.Max(s => s.CommentID) + 1 : 1;
                    entity.TimePosted = comment.TimePosted;
                    entity.Text = comment.Text;

                    //Must Check that EventID and AuthorID are valid values in the Events Table and Players Table
                    //*********************

                    int? id = GuildManager.LoadByID((int)comment.GuildId).GuildId;
                    if (id < 0 || id == null) //If -99, Must not be a primary key with the EventID (foreign key value) in Event Table
                        throw new Exception();
                    else
                        entity.GuildId = comment.GuildId == null ? -1 : (int)comment.GuildId;

                    id = UserManager.LoadById(comment.AuthorID).UserID;
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in Player Table
                        throw new Exception();
                    else
                        entity.AuthorID = comment.AuthorID;

                    //*********************



                    // IMPORTANT - BACK FILL THE ID
                    comment.CommentID = entity.CommentID;

                    dc.tblGuildComments.Add(entity);
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
        public static int Update(GuildComment comment, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGuildComment entity = dc.tblGuildComments.Where(e => e.CommentID == comment.CommentID).FirstOrDefault();
                    entity.TimePosted = comment.TimePosted;
                    entity.Text = comment.Text;

                    //Must Check that EventID and AuthorID are valid values in the Events Table and Players Table
                    //*********************

                    int? id = GuildManager.LoadByID((int)comment.GuildId).GuildId;
                    if (id < 0 || id == null) //If -99, Must not be a primary key with the EventID (foreign key value) in Event Table
                        throw new Exception();
                    else
                        entity.GuildId = comment.GuildId == null ? -1 : (int)comment.GuildId;

                    id = UserManager.LoadById(comment.AuthorID).UserID;
                    if (id == -99) //If -99, Must not be a primary key with the AuthorID (foreign key value) in Player Table
                        throw new Exception();
                    else
                        entity.AuthorID = comment.AuthorID;

                    //*********************

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
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGuildComment entity = dc.tblGuildComments.Where(e => e.CommentID == id).FirstOrDefault();

                    dc.tblGuildComments.Remove(entity);
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
        public static GuildComment LoadById(int id)
        {
            try
            {
                GuildComment comment = new GuildComment();
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblGuildComment entity = dc.tblGuildComments.Where(e => e.CommentID == id).FirstOrDefault();
                    comment.CommentID = id;
                    comment.TimePosted = entity.TimePosted;
                    comment.Text = entity.Text;

                    comment.AuthorID = entity.AuthorID;
                    comment.GuildId = entity.GuildId;


                    return comment;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<GuildComment> LoadByGuildID(int guildID)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    return dc.tblGuildComments
                             .Where(c => c.GuildId == guildID)
                             .Select(c => new GuildComment
                             {
                                 CommentID = c.CommentID,
                                 TimePosted = c.TimePosted,
                                 Text = c.Text,
                                 AuthorID = c.AuthorID,
                                 GuildId = c.GuildId
                             }).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<GuildComment> Load()
        {
            try
            {
                List<GuildComment> list = new List<GuildComment>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from c in dc.tblGuildComments
                     select new
                     {
                         c.CommentID,
                         c.TimePosted,
                         c.Text,
                         c.AuthorID,
                         c.GuildId
                     })
                     .ToList()
                     .ForEach(comment => list.Add(new GuildComment
                     {
                         CommentID = comment.CommentID,
                         TimePosted = comment.TimePosted,
                         Text = comment.Text,
                         AuthorID = comment.AuthorID,
                         GuildId = comment.GuildId
                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}

