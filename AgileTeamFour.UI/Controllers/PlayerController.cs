using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using Microsoft.AspNetCore.Mvc;


namespace AgileTeamFour.Web.Controllers
{
    public class PlayerController : Controller
    {
        // GET: Player
        public ActionResult Index()
        {
            try
            {
                var players = PlayerManager.Load();
                ViewBag.Title = "List of Players";
                return View(players);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Player>());
            }
        }

       
        public ActionResult Details(int id)
        {
            try
            {
                var player = PlayerManager.LoadById(id);
                if (player.PlayerID == -99)
                {
                    return NotFound();
                }

                ViewBag.Title = $"Details for {player.UserName}";
                return View(player);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return NotFound();
            }
        }

        
        public ActionResult Create()
        {
            ViewBag.Title = "Create a New Player";
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlayerManager.Insert(player);
                    return RedirectToAction(nameof(Index));
                }
                return View(player);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(player);
            }
        }

       
        public ActionResult Edit(int id)
        {
            try
            {
                var player = PlayerManager.LoadById(id);
                if (player.PlayerID == -99)
                {
                    return NotFound();
                }

                ViewBag.Title = $"Edit Player {player.UserName}";
                return View(player);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return NotFound();
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Player player)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PlayerManager.Update(player);
                    return RedirectToAction(nameof(Index));
                }
                return View(player);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(player);
            }
        }

        
        public ActionResult Delete(int id)
        {
            try
            {
                var player = PlayerManager.LoadById(id);
                if (player.PlayerID == -99)
                {
                    return NotFound();
                }

                ViewBag.Title = $"Delete Player {player.UserName}";
                return View(player);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return NotFound();
            }
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PlayerManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
