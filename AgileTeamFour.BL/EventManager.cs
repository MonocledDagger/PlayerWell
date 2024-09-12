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
                                string eventType,
                                string platform,
                                string description,
                                DateTime time,
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
                    EventType = eventType,
                    Platform = platform,
                    Description = description,
                    DateTime = time,

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
                    entity.EventType = events.EventType;
                    entity.Platform = events.Platform;
                    entity.Description = events.Description;
                    entity.DateTime = events.DateTime;



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
                        entity.EventType = events.EventType;
                        entity.Platform = events.Platform;
                        entity.Description = events.Description;
                        entity.DateTime = events.DateTime;
                        entity.MaxPlayers = events.MaxPlayers;


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
                            MaxPlayers = entity.MaxPlayers,
                            EventName = entity.EventName,
                            Server=entity.Server,
                            EventType=entity.EventType,
                            DateTime = entity.DateTime,
                            Platform = entity.Platform,
                            Description = entity.Description,
                            

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
                         e.EventType,
                         e.MaxPlayers,
                         e.Server,
                         e.Platform,
                         e.Description,
                         e.DateTime

                     })
                     .ToList()
                     .ForEach(events => list.Add(new Events
                     {
                         EventID = events.EventID,
                         GameID = events.GameID,
                         EventName = events.EventName,
                         EventType = events.EventType,
                         MaxPlayers = events.MaxPlayers,
                         Server = events.Server,
                         Platform = events.Platform,
                         Description = events.Description,
                         DateTime = events.DateTime

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





