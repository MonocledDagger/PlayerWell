using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL
{
    public static class GameManager
    {
        public static int Insert(ref int gameID, string gameName, string platform, string description, string picture, string genre,
                                   bool rollback = false)
        {
            try
            {
                Models.Game game = new Models.Game
                {
                    GameID = gameID,
                    GameName = gameName,
                    Platform = platform,
                    Description = description,
                    Picture = picture,
                    Genre = genre,


                };

                int results = Insert(game, rollback);



                gameID = game.GameID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        

        public static int Insert(Models.Game game, bool rollback = false)
        {
            try
            {
                int results = 0;
                //Need to Scaffold
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();




                    tblGame entity = new tblGame();

                    //Assign GameID
                    entity.GameID = dc.tblGames.Any() ? dc.tblGames.Max(s => s.GameID) + 1 : 1;
                    entity.GameName = game.GameName;
                    entity.Platform = game.Platform;
                    entity.Description = game.Description;
                    entity.Picture = game.Picture;
                    entity.Genre = game.Genre;
                    

                    //IMPORTANT - BACK FILL THE ID
                    game.GameID = entity.GameID;

                    dc.tblGames.Add(entity);
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

       



        public static int Update(Models.Game game, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblGame entity = dc.tblGames.FirstOrDefault(g => g.GameID == game.GameID);

                    if (entity != null)
                    {
                        entity.GameID = game.GameID;
                        entity.GameName = game.GameName;
                        entity.Picture = game.Picture;
                        entity.Description = game.Description;
                        entity.Genre = game.Genre;
                        entity.Platform = game.Platform;


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


        public static int Delete(int GameID, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblGame row = dc.tblGames.FirstOrDefault(d => d.GameID == GameID);


                    dc.tblGames.Remove(row);

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


        public static Models.Game LoadByID(int GameID)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblGame entity = dc.tblGames.FirstOrDefault(g => g.GameID == GameID);

                    if (entity != null)
                    {
                        return new Models.Game
                        {
                            GameID = entity.GameID,
                            GameName = entity.GameName,
                            Platform = entity.Platform,
                            Description = entity.Description,
                            Picture = entity.Picture,
                            Genre = entity.Genre,

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

        public static List<Models.Game> Load()
        {
            try
            {
                List<Models.Game> list = new List<Models.Game>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from g in dc.tblGames
                     select new
                     {
                         g.GameID,
                         g.GameName,
                         g.Platform,
                         g.Description,
                         g.Picture,
                         g.Genre

                     })
                     .ToList()
                     .ForEach(game => list.Add(new Models.Game
                     {
                         GameID = game.GameID,
                         GameName = game.GameName,
                         Platform = game.Platform,
                         Description = game.Description,
                         Picture = game.Picture,
                         Genre = game.Genre


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
