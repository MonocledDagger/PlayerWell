using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
//I followed the PlayerManager code, even though it was all commented out, just to see if I could start working on the controllers and commit my changes.
//    I will comment on my work as well,so it won’t affect anything when someone tries to rebuild the project
namespace AgileTeamFour.Web.Controllers
{
    public class PlayerController : Controller
    {
        //// GET: Player
        //public ActionResult Index()
        //{
        //    List<Player> players = PlayerManager.Load();
        //    return View(players);
        //}

        //// GET: Player/Details/5
        //public ActionResult Details(int id)
        //{
        //    Player player = PlayerManager.LoadById(id);
        //    if (player.PlayerID == -99)
        //    {
        //        return NotFound();
        //    }
        //    return View(player);
        //}

        //// GET: Player/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Player/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Player player)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            PlayerManager.Insert(player);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(player);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Player/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    Player player = PlayerManager.LoadById(id);
        //    if (player.PlayerID == -99)
        //    {
        //        return NotFound();
        //    }
        //    return View(player);
        //}

        //// POST: Player/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Player player)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            PlayerManager.Update(player);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View(player);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Player/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    Player player = PlayerManager.LoadById(id);
        //    if (player.PlayerID == -99)
        //    {
        //        return NotFound();
        //    }
        //    return View(player);
        //}

        //// POST: Player/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        PlayerManager.Delete(id);
                
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
