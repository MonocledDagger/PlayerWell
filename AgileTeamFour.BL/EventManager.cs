using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgileTeamFour.BL
{
    public static class EventManager
    {
        public static int Insert(ref int eventID,
                                int gameID,
                                string eventName,
                                string server,
                                int maxPlayers,
                                string type,
                                string platform,
                                string description,
                                DateTime time,
                                int AuthorId,
                                bool rollback = false)
        {
            try
            {
                //"events" cannot be "event" or it causes errors
                Events events = new Events
                {
                    EventID = eventID,
                    GameID = gameID,
                    EventName = eventName,
                    Server = server,
                    MaxPlayers = maxPlayers,
                    Platform = platform,
                    Description = description,
                    DateTime = time,
                    Type = type,
                    AuthorId = AuthorId

                };

                int results = Insert(events, rollback);



                eventID = events.EventID;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static int Insert(Events events, bool rollback = false)
        {
            try
            {
                int results = 0;
                //Need to Scaffold
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();




                    tblEvent entity = new tblEvent();

                    //Generate EventID
                    entity.EventID = dc.tblEvents.Any() ? dc.tblEvents.Max(s => s.EventID) + 1 : 1;
                    entity.GameID = events.GameID;
                    entity.EventName = events.EventName;
                    entity.Server = events.Server;
                    entity.MaxPlayers = events.MaxPlayers;
                    entity.Platform = events.Platform;
                    entity.Description = events.Description;
                    entity.DateTime = events.DateTime;
                    entity.Type = events.Type;
                    entity.AuthorId = events.AuthorId;



                    events.GameID = (int)entity.GameID;

                    dc.tblEvents.Add(entity);
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

        public static int Update(Events events, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblEvent entity = dc.tblEvents.FirstOrDefault(e => e.EventID == events.EventID);

                    if (entity != null)
                    {
                        entity.EventID = events.EventID;
                        entity.GameID=events.GameID;
                        entity.EventName = events.EventName;
                        entity.Server = events.Server;
                        entity.Platform = events.Platform;
                        entity.Description = events.Description;
                        entity.DateTime = events.DateTime;
                        entity.MaxPlayers = events.MaxPlayers;
                        entity.Type = events.Type;
                        entity.AuthorId = events.AuthorId;

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


        public static int Delete(int EventID, bool rollback = false)
        {

            try
            {
                int results = 0;
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    IDbContextTransaction dbContextTransaction = null;
                    if (rollback) dbContextTransaction = dc.Database.BeginTransaction();

                    tblEvent row = dc.tblEvents.FirstOrDefault(d => d.EventID == EventID);


                    dc.tblEvents.Remove(row);

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


        public static Events LoadByID(int id)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    tblEvent entity = dc.tblEvents.FirstOrDefault(e => e.EventID == id);

                    if (entity != null)
                    {
                        return new Events
                        {
                            EventID = entity.EventID,
                            GameID = entity.GameID,
                            MaxPlayers = (int)entity.MaxPlayers,
                            EventName = entity.EventName,
                            Server=entity.Server,
                            DateTime = entity.DateTime,
                            Platform = entity.Platform,
                            Description = entity.Description,
                            Type = entity.Type,
                            AuthorId = entity.AuthorId

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

        public static List<Events> Load()
        {
            try
            {
                List<Events> list = new List<Events>();

                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    (from e in dc.tblEvents
                     select new
                     {
                         e.EventID,
                         e.GameID,
                         e.EventName,
                         e.MaxPlayers,
                         e.Server,
                         e.Platform,
                         e.Description,
                         e.DateTime,
                         e.Type,
                         e.AuthorId

                     })
                     .ToList()
                     .ForEach(events => list.Add(new Events
                     {
                         EventID = events.EventID,
                         GameID = events.GameID,
                         EventName = events.EventName,
                         MaxPlayers = (int)events.MaxPlayers,
                         Server = events.Server,
                         Platform = events.Platform,
                         Description = events.Description,
                         DateTime = events.DateTime,
                         Type = events.Type,
                         AuthorId = events.AuthorId

                     }));
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Added  method to get Author Name of Event
        public static string GetAuthorName(int eventID)
        {
            try
            {
                using (AgileTeamFourEntities dc = new AgileTeamFourEntities())
                {
                    // Query the PlayerEvent table for the specified eventID
                    int authorId = EventManager.LoadByID(eventID)?.AuthorId ?? UserManager.Load().FirstOrDefault().UserID;

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





