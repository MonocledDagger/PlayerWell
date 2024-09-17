
using AgileTeamFour.BL.Models;
using System.Xml;

namespace AgileTeamFour.BL
{
    public static class PlayerManager
    {
        
        //So far, none of these methods change primary keys, other than delete, which deletes a primary key in the deleted row
        //When changing a primary key, required to update foreign keys that reference that primary key first to ensure referential integrity
        //Adding a value to any foreign key requires the foreign key value first exist as a a primary key value of the referenced table
        //Reviews.AuthorID uses Players.PlayerID as a foreign key
        //Reviews.RecipientID uses Players.PlayerID as a foreign key
        //Comments.AuthorID uses Players.PlayerID as a foreign key
        //PlayerEvent.PlayerID uses Players.PlayerID as a foreign key
        public static int Insert(Player player, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayer entity = new tblPlayer();
                    entity.PlayerID = dc.tblPlayers.Any() ? dc.tblPlayers.Max(s => s.PlayerID) + 1 : 1;
                    entity.UserName = player.UserName;
                    entity.Email = player.Email;
                    entity.Password = player.Password;
                    entity.IconPic = player.IconPic;
                    entity.Bio = player.Bio;
                    entity.DateTime = player.DateTime;

                    // IMPORTANT - BACK FILL THE ID
                    player.PlayerID = entity.PlayerID;

                    dc.tblPlayers.Add(entity);
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
        public static int Update(Player player, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblPlayer entity = dc.tblPlayers.Where(e => e.PlayerID == player.PlayerID).FirstOrDefault();
                    entity.UserName = player.UserName;
                    entity.Email = player.Email;
                    entity.Password = player.Password;
                    entity.IconPic = player.IconPic;
                    entity.Bio = player.Bio;
                    entity.DateTime = player.DateTime;

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
        //public static int Delete(int id, bool rollback = false) 
        //{
        ////What happens when a player is deleted?
        //        //Reviews.AuthorID uses Players.PlayerID as a foreign key
        //        //Reviews.RecipientID uses Players.PlayerID as a foreign key
        //        //Comments.AuthorID uses Players.PlayerID as a foreign key
        //        //PlayerEvent.PlayerID uses Players.PlayerID as a foreign key
        ////Rows mentioned in comments above, need to be either changed or deleted before a PlayerID is deleted
        //    try
        //    {
        //        int results = 0;
        //        using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
        //        {
        //            IDbContextTransaction transaction = null;
        //            if (rollback) transaction = dc.Database.BeginTransaction();

        //            tblPlayer entity = dc.tblPlayers.Where(e => e.PlayerID == id).FirstOrDefault();
                    

        //            dc.tblPlayers.Remove(entity);
        //            results = dc.SaveChanges();

        //            if (rollback) transaction.Rollback();

        //        }
        //        return results;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public static Player LoadById(int id)
        {
            try
            {
                Player player = new Player();
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblPlayer entity = dc.tblPlayers.Where(e => e.PlayerID == id).FirstOrDefault();
                    if (entity != null)
                    {
                        player.PlayerID = id;
                        player.UserName = entity.UserName;
                        player.Email = entity.Email;
                        player.Password = entity.Password;
                        player.IconPic = entity.IconPic;
                        player.Bio = entity.Bio;
                        player.DateTime = entity.DateTime == null ? DateTime.MinValue : (DateTime)entity.DateTime;
                    }
                    else
                    {
                        //ID of -99 means no player with the loadedID exists
                        player.PlayerID = -99;
                    }

                    return player;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static List<Player> Load()
        {
            try
            {
                List<Player> list = new List<Player>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from p in dc.tblPlayers
                     select new
                     {
                         p.PlayerID,
                         p.UserName,
                         p.Email,
                         p.Password,
                         p.IconPic,
                         p.Bio,
                         p.DateTime
                })
                     .ToList()
                     .ForEach(player => list.Add(new Player
                     {

                         PlayerID = player.PlayerID,
                         UserName = player.UserName,
                         Email = player.Email,
                         Password = player.Password,
                         IconPic = player.IconPic,
                         Bio = player.Bio,
                         DateTime = player.DateTime == null ? DateTime.MinValue : (DateTime)player.DateTime
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

