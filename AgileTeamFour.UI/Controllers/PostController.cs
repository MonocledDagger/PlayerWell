
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.Web.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Index()
        {

            return View(PostManager.Load());

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
        public IActionResult Create(Post post)
        {

            try
            {
                User user = GetLoggedInUser();
                ViewBag.Title = "Create a User";

                post.AuthorID = user.UserID;
                post.TimePosted = DateTime.Now;
                // Set image to an empty string if it is null
                post.Image = post.Image ?? "";
                int result = PostManager.Insert(post);
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





    

