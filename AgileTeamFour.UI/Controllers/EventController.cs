using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgileTeamFour.Web.Controllers
{
    public class EventController : Controller
    {

        //public ActionResult Index()
        //{
        //    ViewBag.Title = "List of Events";
        //    return View(EventManager.Load());
        //}

        public ActionResult Index()
        {
            var events = EventManager.Load(); // Load the events from the manager

            // Map each Events object to EventDetailsVM
            var eventDetailsVMs = events.Select(e => new EventDetailsVM
            {
                Event = e, // Assign the event object
                Game = GameManager.LoadByID(e.GameID) // Load the corresponding Game object for each event
            }).ToList();

            ViewBag.Title = "List of Events";

            // Pass the list of EventDetailsVM to the view
            return View(eventDetailsVMs);
        }



        public ActionResult Details(int id)
        {
            var eventItem = EventManager.LoadByID(id);
            if (eventItem == null)
            {
                return NotFound();
            }

           
            var game = GameManager.LoadByID(eventItem.GameID);
            var playerEvents = PlayerEventManager.LoadByEventID(id); 
            var comments = CommentManager.LoadByEventID(id); 

            // Create the ViewModel
            var eventDetailsVM = new EventDetailsVM
            {
                Event = eventItem,
                Game = game,
                PlayerEvents = playerEvents ?? new List<PlayerEvent>(), 
                Comments = comments ?? new List<Comment>()
            };

            ViewBag.Title = "Details for " + eventItem.EventName;

            // Pass the ViewModel to the view
            return View(eventDetailsVM);
        }


        public ActionResult Create()
        {
            EventVM vm = new EventVM();
            ViewBag.Title = "Create an Event";
            return View(vm);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventVM eventCreateVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int eventID = 0;
                    EventManager.Insert(ref eventID,
                        eventCreateVM.Event.GameID,
                        eventCreateVM.Event.EventName,
                        eventCreateVM.Event.Server, 
                        eventCreateVM.Event.MaxPlayers,
                        eventCreateVM.Event.Type,
                        eventCreateVM.Event.Platform, 
                        eventCreateVM.Event.Description, 
                        eventCreateVM.Event.DateTime);
                    return RedirectToAction(nameof(Index));
                }
                return View(eventCreateVM);
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Edit(int id)
        {
            var eventItem = EventManager.LoadByID(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            var allGames = GameManager.Load();


            // Create the ViewModel
            var eventDetailsVM = new EventVM
            {
                Event = eventItem,
                Games = allGames,


            };

            ViewBag.Title = "Edit " + eventItem.EventName;

            // Pass the ViewModel to the view
            return View(eventDetailsVM);
            
           
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
                eventItem.GameID = model.Event.GameID; // Assuming single selection
                eventItem.Server = model.Event.Server;
                eventItem.MaxPlayers = model.Event.MaxPlayers;
                eventItem.Type = model.Event.Type;
                eventItem.Platform = model.Event.Platform;
                eventItem.Description = model.Event.Description;
                eventItem.DateTime = model.Event.DateTime;

                // Save changes
                EventManager.Update(eventItem);

                return RedirectToAction("Index");
            }

            // Reload the games if the model is invalid to re-populate the form
            model.Games = GameManager.Load();
            return View(model);
        }

        //public ActionResult Delete(int id)
        //{
        //    var item = EventManager.LoadByID(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewBag.Title = "Delete Event";
        //    return View(item);
        //}

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
                
                
            };

            ViewBag.Title = "Delete " + eventItem.EventName;

            // Pass the ViewModel to the view
            return View(eventDetailsVM);
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
    }
}
