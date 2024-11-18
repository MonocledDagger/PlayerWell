
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.Web.Controllers
{
    public class PostCommentController : Controller
    {
        public ActionResult Index()
        {

            return View(PostCommentManager.Load());

        }
        [HttpGet]

        public IActionResult Create()
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View();
            }
            else
            {
                TempData["error"] = "Need to be logged in to Post";
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }



        }

        [HttpPost]
        public IActionResult Create(PostComment post)
        {

            try
            {
                User user = GetLoggedInUser();
                ViewBag.Title = "Create a replay";

                post.AuthorID = user.UserID;
                post.TimePosted = DateTime.Now;
                // Set image to an empty string if it is null
                //post.Image = post.Image ?? "";
                int result = PostCommentManager.Insert(post);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create";
                ViewBag.Error = ex.Message;
                return View(post);
            }
        }

        private User GetLoggedInUser()
        {
            return HttpContext.Session.GetObject<User>("user");
        }
    }
}





    

