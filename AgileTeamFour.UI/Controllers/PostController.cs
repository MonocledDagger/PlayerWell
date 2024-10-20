using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgileTeamFour.Web.Controllers
{
    public class PostController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Posts";
            return View(PostManager.Load());

        }
      

        public ActionResult Details(int id)
        {


           // var item = PostManager.LoadById(id);

            return View(PostManager.LoadById(id));

        }


        public ActionResult Create()
        {
            ViewBag.Title = "Create a Post";
            if (Authenticate.IsAuthenticated(HttpContext, "user"))
            {
                return View();
            }
            else
            {
                TempData["error"] = "Need to be a user to Post";
                return RedirectToAction("Index", "Post");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //int PostID = 0;
                    //PostManager.Insert(post.AuthorID,post.TimePosted,post.Image,post.Text,false);
                    //return RedirectToAction(nameof(Index));
                    Post newPost = new Post
                    {
                        PostID = post.PostID,
                        AuthorID = post.AuthorID,
                        TimePosted = post.TimePosted,
                        Image=post.Image,   
                        Text = post.Text,
                       
                    };

                    PostManager.Insert(newPost);


                }
                return View(post);
            }
            catch
            {
                return View();
            }
        }


    //    public ActionResult Edit(int id)
    //    {
    //        var items = GameManager.LoadByID(id);

    //        ViewBag.Title = "Edit ";
    //        if (Authenticate.IsAuthenticated(HttpContext, "admin"))
    //        {
    //            return View(items);
    //        }
    //        else
    //        {
    //            TempData["error"] = "Need admin rights to view page";
    //            return RedirectToAction("Index", "Game");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
    //        }


    //    }


    //    [HttpPost]
    //    [ValidateAntiForgeryToken]

    //    public ActionResult Edit(int id, Game game, bool rollback = false)
    //    {

    //        try
    //        {
    //            int result = GameManager.Update(game, rollback);
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch (Exception ex)
    //        {

    //            ViewBag.Error = ex.Message;
    //            return View(game);
    //        }
    //    }


    //    public ActionResult Delete(int id)
    //    {
    //        Game game = GameManager.LoadByID(id);
    //        if (game == null)
    //        {
    //            return NotFound();
    //        }
    //        if (Authenticate.IsAuthenticated(HttpContext, "admin"))
    //        {
    //            return View(game);
    //        }
    //        else
    //        {
    //            TempData["error"] = "Need admin rights to view page";
    //            return RedirectToAction("Index", "Game");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
    //        }

    //    }


    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(int id)
    //    {
    //        try
    //        {
    //            GameManager.Delete(id);
    //            return RedirectToAction(nameof(Index));
    //        }
    //        catch
    //        {
    //            return View();
    //        }
    //    }
    }
}
