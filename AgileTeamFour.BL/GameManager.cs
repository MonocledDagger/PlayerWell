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
                Game game = new Game
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



        public static int Insert(Game game, bool rollback = false)
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
                    entity.GameID = dc.tblGames.Any() ? dc.tblGames.Max(s => s.Id) + 1 : 1;
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
    }
}
