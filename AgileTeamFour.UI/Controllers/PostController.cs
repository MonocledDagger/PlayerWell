using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
//AgileTeamFour.UI.ViewModels.PostVM
namespace AgileTeamFour.Web.Controllers
{
    public class PostController : Controller
    {


        //public IActionResult Index()
        //{
        //    // Assuming you fetch a list of posts and map them to PostVM
        //    var posts = PostManager.Load(); // Retrieve a list of Post from the service or database

        //    var postVMs = posts.Select(post => new PostVM
        //    {
        //        currentPlayers=post.PostID,
        //        PostID = post.PostID,
        //        AuthorID = post.AuthorID,
        //        TimePosted = post.TimePosted,
        //        Image = post.Image,
        //        Text = post.Text
        //        // Other mapping logic here
        //    }).ToList();

        //    return View(postVMs); // Now pass the collection of PostVM to the view
        //}


        public ActionResult Index()
        {

            return View(PostManager.Load());

        }


        //public ActionResult Index()
        //{
        //    var posts = PostManager.Load();

        //    // For each post, get the review summary
        //    foreach (var post in posts)
        //    {
        //        post.ReviewSummary = PostManager.GetReviewSummaryForUser(post.AuthorID);
        //    }

        //    return View(posts);
        //}



        //public ActionResult Details(int id)
        //{


        //   // var item = PostManager.LoadById(id);

        //    return View(PostManager.LoadById(id));

        //}


        //public ActionResult Create()
        //{
        //    ViewBag.Title = "Create a Post";
        //    if (Authenticate.IsAuthenticated(HttpContext, "user"))
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        TempData["error"] = "Need to be a user to Post";
        //        return RedirectToAction("Index", "Post");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Post post)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //int PostID = 0;
        //            //PostManager.Insert(post.AuthorID,post.TimePosted,post.Image,post.Text,false);
        //            //return RedirectToAction(nameof(Index));
        //            Post newPost = new Post
        //            {
        //                PostID = post.PostID,
        //                AuthorID = post.AuthorID,
        //                TimePosted = post.TimePosted,
        //                Image=post.Image,   
        //                Text = post.Text,

        //            };

        //            PostManager.Insert(newPost);


        //        }
        //        return View(post);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


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
