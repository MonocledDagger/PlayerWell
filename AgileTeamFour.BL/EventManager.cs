using AgileTeamFour.BL.Models;
using Microsoft.EntityFrameworkCore.Storage;
using AgileTeamFour.PL;
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
                Event events = new Event
                {
                    EventID = eventID,
                    GameID = gameID,
                    EventName = eventName,
                    Server = server,
                    MaxPlayers = maxPlayers,
                    EventType = eventType,
                    Platform = platform,
                    Description = description,
                    time = time,

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



        public static int Insert(Event events, bool rollback = false)
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
                    entity.EventID = dc.tblEvents.Any() ? dc.tblEvents.Max(s => s.Id) + 1 : 1;
                    entity.GameID = events.GameID;
                    entity.EventName = events.EventName;
                    entity.Server = events.Server;
                    entity.MaxPlayers = events.MaxPlayers;
                    entity.EventType = events.EventType;
                    entity.Platform = events.Platform;
                    entity.Description = events.Description;
                    entity.Time = events.time;



                    events.GameID = entity.GameID;

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
    }
}





