using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AgileTeamFour.Web.Controllers
{
    public class EventController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Title = "List of Events";
            return View(EventManager.Load());
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
            ViewBag.Title = "Create an Event";
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Events events)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int eventID = 0;
                    EventManager.Insert(ref eventID, events.GameID, events.EventName, events.Server, events.MaxPlayers, events.Type, events.Platform, events.Description, events.DateTime);
                    return RedirectToAction(nameof(Index));
                }
                return View(events);
            }
            catch
            {
                return View();
            }
        }

       
        public ActionResult Edit(int id)
        {
            var item = EventManager.LoadByID(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Edit Event";
            return View(item);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Events events, bool rollback = false)
        {
            try
            {
                int result = EventManager.Update(events, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(events);
            }
        }

       
        public ActionResult Delete(int id)
        {
            var item = EventManager.LoadByID(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewBag.Title = "Delete Event";
            return View(item);
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
