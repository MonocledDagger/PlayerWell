using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgileTeamFour.Web.Controllers
{
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index()
        {
            ViewBag.Title = "List of Games";
            return View(GameManager.Load());

        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {


            var item = GameManager.LoadByID(id);
            //ViewBag.Title = "Details for " + item.FullName;
            return View(GameManager.LoadByID(id));

        }

        // GET: Game/Create
        public ActionResult Create()
        {
            ViewBag.Title = "Create a Game";
            return View();
        }

        // POST: Game/Create
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

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            var items = GameManager.LoadByID(id);

            ViewBag.Title = "Edit ";
            return View(items);

        }
    
        // POST: Game/Edit/5
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

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            Game game = GameManager.LoadByID(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Game/Delete/5
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
