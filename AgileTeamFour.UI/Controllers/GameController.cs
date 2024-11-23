using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgileTeamFour.Web.Controllers
{
    public class GameController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.Title = "List of Games";

            //Helps to determine which Edit, Details, and Delete links to display in view for those that are not admins
            TempData["IsAdmin"] = Authenticate.IsAdmin(HttpContext); 
            return View(GameManager.Load());

        }
        public ActionResult IndexCard()
        {
            ViewBag.Title = "List of Games";
            return View(GameManager.Load());

        }

      
        public ActionResult Details(int id)
        {


            var item = GameManager.LoadByID(id);
            
            return View(GameManager.LoadByID(id));

        }

        
        public ActionResult Create()
        {
            ViewBag.Title = "Create a Game";
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                return View();
            }
            else
            {
                TempData["error"] = "Need admin rights to view page";
                return RedirectToAction("Index", "Game");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }      

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Game game)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int gameID = 0;
                    GameManager.Insert(ref gameID, game.GameName, game.Platform, game.Description, game.Picture, game.Genre);
                    return RedirectToAction(nameof(Index));
                }
                return View(game);
            }
            catch
            {
                return View();
            }
        }

      
        public ActionResult Edit(int id)
        {
            var items = GameManager.LoadByID(id);

            ViewBag.Title = "Edit ";
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                return View(items);
            }
            else
            {
                TempData["error"] = "Need admin rights to view page";
                return RedirectToAction("Index", "Game");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
            

        }
    
     
        [HttpPost]
       [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Game game, bool rollback = false)
        {

            try
            {
                int result = GameManager.Update(game, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(game);
            }
        }

        
        public ActionResult Delete(int id)
        {
            Game game = GameManager.LoadByID(id);
            if (game == null)
            {
                return NotFound();
            }
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                return View(game);
            }
            else
            {
                TempData["error"] = "Need admin rights to view page";
                return RedirectToAction("Index", "Game");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
            
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                GameManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
