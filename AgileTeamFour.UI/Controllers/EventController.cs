﻿using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace AgileTeamFour.Web.Controllers
{
    public class EventController : Controller
    {
        //public ActionResult Index()
        //{
        //    var events = EventManager.Load(); // Load the events from the manager

        //    // Map each Events object to EventDetailsVM
        //    var eventDetailsVMs = events.Select(e => new EventDetailsVM
        //    {
        //        Event = e, // Assign the event object
        //        Game = GameManager.LoadByID(e.GameID), // Load Game object for each event
        //        AuthorName = UserManager.LoadById(e.AuthorId).UserName, // Load AuthorName for Event Filtering
        //        Guild = e.GuildId.HasValue ? GuildManager.LoadByID(e.GuildId.Value) : null // Check for null value before loading
        //    }).ToList();

        //    ViewBag.Title = "List of Events";

        //    // Pass the list of EventDetailsVM to the view
        //    return View("Index", eventDetailsVMs);
        //}

        //public ActionResult Index2()
        //{
        //    var events = EventManager.Load(); // Load the events from the manager

        //    // Map each Events object to EventDetailsVM
        //    var eventDetailsVMs = events.Select(e => new EventDetailsVM
        //    {
        //        Event = e, // Assign the event object
        //        Game = GameManager.LoadByID(e.GameID), // Load the corresponding Game object for each event
        //        AuthorName = UserManager.LoadById(e.AuthorId).UserName, // Load AuthorName for Event Filtering
        //        Guild = e.GuildId.HasValue ? GuildManager.LoadByID(e.GuildId.Value) : null // Check for null value before loading
        //    }).ToList();

        //    ViewBag.Title = "List of Events";

        //    // Pass the list of EventDetailsVM to the view
        //    return View(eventDetailsVMs);
        //}

        public ActionResult Index(string gameName,
                                   string authorName,
                                   string guildName,
                                   DateTime? startDate,
                                   DateTime? endDate)
        {
            var events = EventManager.Load(); // Load the events from the manager

            // Map each Events object to EventDetailsVM
            var eventDetailsVMs = events.Select(e => new EventDetailsVM
            {
                Event = e, // Assign the event object
                Game = GameManager.LoadByID(e.GameID), // Load the corresponding Game object for each event
                AuthorName = UserManager.LoadById(e.AuthorId).UserName, // Load AuthorName for Event Filtering
                Guild = e.GuildId.HasValue ? GuildManager.LoadByID(e.GuildId.Value) : null // Check for null value before loading
            }).ToList();

            ViewBag.Title = "List of Events";

            // Check if any of the filters have been set
            bool hasFilters = !string.IsNullOrEmpty(gameName) ||
                              !string.IsNullOrEmpty(authorName) ||
                              !string.IsNullOrEmpty(guildName) ||
                              startDate.HasValue ||
                              endDate.HasValue;

            // Apply filters if any are provided
            if (hasFilters)
            {
                // Start checks for event filtering passed back in the url through key value pairs
                if (!string.IsNullOrEmpty(gameName))
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Game.GameName.Contains(gameName)).ToList();

                if (!string.IsNullOrEmpty(authorName))
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.AuthorName.Contains(authorName)).ToList();

                if (!string.IsNullOrEmpty(guildName) && guildName != "No Guild")
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Guild != null && e.Guild.GuildName.Contains(guildName)).ToList();
                else if (guildName == "No Guild")
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Guild == null).ToList();

                if (startDate.HasValue)
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Event.DateTime >= startDate.Value).ToList();

                if (endDate.HasValue)
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Event.DateTime <= endDate.Value).ToList();
            }

            eventDetailsVMs = eventDetailsVMs.OrderBy(e => e.Event.DateTime).Where(e => e.Event.DateTime >= DateTime.Now).ToList();


            // Pass the list of EventDetailsVM to the view
            return View("Index", eventDetailsVMs);
        }

        public ActionResult Index2(string gameName,
                                   string authorName,
                                   string guildName,
                                   DateTime? startDate,
                                   DateTime? endDate)
        {
            var events = EventManager.Load(); // Load the events from the manager

            // Map each Events object to EventDetailsVM
            var eventDetailsVMs = events.Select(e => new EventDetailsVM
            {
                Event = e, // Assign the event object
                Game = GameManager.LoadByID(e.GameID), // Load the corresponding Game object for each event
                AuthorName = UserManager.LoadById(e.AuthorId).UserName, // Load AuthorName for Event Filtering
                Guild = e.GuildId.HasValue ? GuildManager.LoadByID(e.GuildId.Value) : null // Check for null value before loading
            }).ToList();

            ViewBag.Title = "List of Events";

            // Check if any of the filters have been set
            bool hasFilters = !string.IsNullOrEmpty(gameName) ||
                              !string.IsNullOrEmpty(authorName) ||
                              !string.IsNullOrEmpty(guildName) ||
                              startDate.HasValue ||
                              endDate.HasValue;

            // Apply filters if any are provided
            if (hasFilters)
            {
                // Start checks for event filtering passed back in the url through key value pairs
                if (!string.IsNullOrEmpty(gameName))
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Game.GameName.Contains(gameName)).ToList();

                if (!string.IsNullOrEmpty(authorName))
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.AuthorName.Contains(authorName)).ToList();

                if (!string.IsNullOrEmpty(guildName) && guildName != "No Guild")
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Guild != null && e.Guild.GuildName.Contains(guildName)).ToList();
                else if (guildName == "No Guild")
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Guild == null).ToList();

                if (startDate.HasValue)
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Event.DateTime >= startDate.Value).ToList();

                if (endDate.HasValue)
                    eventDetailsVMs = eventDetailsVMs.Where(e => e.Event.DateTime <= endDate.Value).ToList();
            }

            eventDetailsVMs = eventDetailsVMs.OrderBy(e => e.Event.DateTime).Where(e => e.Event.DateTime >= DateTime.Now).ToList();


            // Pass the list of EventDetailsVM to the view
            return View("Index2", eventDetailsVMs);
        }




        public ActionResult Details(int id)
        {
            var eventItem = EventManager.LoadByID(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            //Get PlayerID from session for sign up
            var user = HttpContext.Session.GetObject<User>("user");
            var playerID = user?.UserID ?? 0;
 
            var game = GameManager.LoadByID(eventItem.GameID);
            var playerEvents = PlayerEventManager.LoadByEventID(id);
            var comments = CommentManager.LoadByEventID(id);
            

            var guild = eventItem.GuildId.HasValue
            ? GuildManager.LoadByID(eventItem.GuildId.Value)
            : null;



            // Count the number of players signed up
            int currentPlayers = playerEvents != null ? playerEvents.Count() : 0;

            // Create the ViewModel
            var eventDetailsVM = new EventDetailsVM
            {
                Event = eventItem,
                Game = game,
                PlayerEvents = playerEvents ?? new List<PlayerEvent>(),
                Comments = comments ?? new List<Comment>(),
                PlayerID = playerID,
                currentPlayers = currentPlayers,
                AuthorName = EventManager.GetAuthorName(id),
                Users = UserManager.Load() ?? new List<User>(),
                Guild = guild
            };


            ViewBag.Title = "Details for " + eventItem.EventName;



            // Pass the ViewModel to the view
            return View(eventDetailsVM);
        }

        public ActionResult Create()
        {
            EventVM vm = new EventVM();

            vm.Guilds = GuildManager.Load();
            ViewBag.Title = "Create an Event";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                var user = HttpContext.Session.GetObject<User>("user");
                int playerID = user.UserID;
                TempData["authorID"] = user.UserID;
                return View(vm);
            }
            else
            {
                TempData["error"] = "Need to be logged in to Create an Event.";
                return RedirectToAction("Index", "Event");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventVM eventCreateVM)
        {   // Check if game already exists in database if not add it
            Game existingGame = GameManager.LoadByID(eventCreateVM.Event.GameID, true);

            if (existingGame == null)
            {
                Game newGame = new Game
                {
                    GameID = eventCreateVM.Event.GameID,
                    GameName = eventCreateVM.Game.GameName,
                    Description = eventCreateVM.Game.Description,
                    Genre = eventCreateVM.Game.Genre,
                    Picture = eventCreateVM.Game.Picture,
                    Platform = eventCreateVM.Event.Platform,
                };

                GameManager.Insert(newGame);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    int eventID = 0;
                    int authorID = 1;
                    eventCreateVM.Event.AuthorId = (int)TempData["authorID"];
                    EventManager.Insert(ref eventID,
                        eventCreateVM.Event.GameID,
                        eventCreateVM.Event.EventName,
                        eventCreateVM.Event.Server,
                        eventCreateVM.Event.MaxPlayers,
                        eventCreateVM.Event.Type,
                        eventCreateVM.Event.Platform,
                        eventCreateVM.Event.Description,
                        eventCreateVM.Event.DateTime,
                        eventCreateVM.Event.AuthorId,
                        eventCreateVM.Event.GuildId);
                    return RedirectToAction(nameof(Index));
                }
                return View(eventCreateVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.InnerException.Message;
                return RedirectToAction("Index", "Event");
                //return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var eventItem = EventManager.LoadByID(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            // Create the ViewModel
            var eventDetailsVM = new EventVM
            {
                Event = eventItem,
                Game = GameManager.LoadByID(eventItem.GameID),
            };

            ViewBag.Title = "Edit " + eventItem.EventName;

            if (Authenticate.IsAuthenticated(HttpContext, eventItem.AuthorId) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                // Pass the ViewModel to the view
                return View(eventDetailsVM);
            }
            else
            {
                TempData["error"] = "Need admin or author rights to view page";
                return RedirectToAction("Index", "Event");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventVM model)
        {
            if (ModelState.IsValid)
            {
                // Update the event using the data from the ViewModel
                var eventItem = EventManager.LoadByID(model.Event.EventID);
                if (eventItem == null)
                {
                    return NotFound();
                }

                // Update the event fields
                eventItem.EventName = model.Event.EventName;
                eventItem.GameID = model.Event.GameID;
                eventItem.Server = model.Event.Server;
                eventItem.MaxPlayers = model.Event.MaxPlayers;
                eventItem.Type = model.Event.Type;
                eventItem.Platform = model.Event.Platform;
                eventItem.Description = model.Event.Description;
                eventItem.DateTime = model.Event.DateTime;
                eventItem.GuildId = model.Event.GuildId;

                // Save changes
                EventManager.Update(eventItem);

                return RedirectToAction("Index");
            }

            else
            {
                return View(model);
            }

        }


        public ActionResult Delete(int id)
        {
            var eventItem = EventManager.LoadByID(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            var game = GameManager.LoadByID(eventItem.GameID);


            // Create the ViewModel
            var eventDetailsVM = new EventDetailsVM
            {
                Event = eventItem,
                Game = game,
                AuthorName = EventManager.GetAuthorName(id)

            };

            ViewBag.Title = "Delete " + eventItem.EventName;

            if (Authenticate.IsAuthenticated(HttpContext, eventItem.AuthorId) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                // Pass the ViewModel to the view
                return View(eventDetailsVM);
            }
            else
            {
                TempData["error"] = "Need admin or author rights to view page";
                return RedirectToAction("Index", "Event");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EventManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EventsForGame(int gameID)
        {
            // Load all events and filter by the provided GameID
            var events = EventManager.Load().Where(e => e.GameID == gameID);

            // Map each filtered event to the EventDetailsVM
            var eventDetailsVMs = events.Select(e => new EventDetailsVM
            {
                Event = e,
                Game = GameManager.LoadByID(e.GameID)
            }).ToList();

            ViewBag.Title = $"Events for Game ID: {gameID}";

            // Pass the filtered list of EventDetailsVM to the view
            return View(eventDetailsVMs);
        }

        [HttpPost]
        public ActionResult SignUp(int EventID, int PlayerID, string Role)
        {
            try
            {
                // Check if the player is already signed up for this event
                var playerList = PlayerEventManager.LoadByEventID(EventID);
                var existingPlayer = playerList.FirstOrDefault(pe => pe.PlayerID == PlayerID);


                // The PlayerID is retrieved from session when the page loads. See ActionResult Details.
                if (PlayerID == 0)
                {
                    //If User isn't signed in
                    TempData["error"] = "Please Sign In Before Joining an Event";
                    // Redirect back to the details page
                    return RedirectToAction("Details", new { id = EventID });


                }
                else if (existingPlayer != null)
                {
                    // Player is already signed up for the event
                    TempData["error"] = "You are already signed up for this event.";
                    return RedirectToAction("Details", new { id = EventID });

                }
                else
                {
                    if (Role == null || Role.IsNullOrEmpty())
                    {
                        Role = "Participant";
                    }
                    // Insert the new player event
                    PlayerEventManager.Insert(PlayerID, EventID, Role);


                    // Redirect back to the details page
                    return RedirectToAction("Details", new { id = EventID });
                }

            }
            catch (Exception ex)
            {
                // Show error message 
                ViewBag.ErrorMessage = "Error while signing up: " + ex.Message;
                return RedirectToAction("Details", new { id = EventID });
            }
        }

        public ActionResult LeaveEvent(int eventID, int playerID)
        {


            // Perform the deletion only if the player is signed in
            if (playerID != 0)
            {
                PlayerEventManager.Delete(playerID, eventID); // Delete by EventID and PlayerID
            }
            //return RedirectToAction("Details", new { id = id }); // Redirect back to event details
            return RedirectToAction("Details", new { id = eventID });
        }
    
        
        [HttpPost]
        public ActionResult InviteEvent(int eventID, string playerName)
        {   // Try adding the player and return a message with reault of attempt
            TempData["SuccessMessage"] = null;
            TempData["ErrorMessage"] = null;
            string resultMessage = PlayerEventManager.InviteEvent(playerName, eventID);

            if (resultMessage == "Player invited successfully")
                TempData["SuccessMessage"] = resultMessage; // Have to use temp data as viewbag
            else
                TempData["ErrorMessage"] = resultMessage;   // Does not persist across requests
                                                            // Whicih occirs with the RedirectAction
            return RedirectToAction("Details", new { id = eventID });
        }
    }
}

