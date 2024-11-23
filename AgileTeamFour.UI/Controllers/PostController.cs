
using AgileTeamFour.PL;
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
            using (var dc = new AgileTeamFourEntities())
            {

                if (Authenticate.IsAuthenticated(HttpContext))
                {   try
                    {
                        User user = GetLoggedInUser();
                        ViewBag.Title = "Create a User";
                        post.PostID=post.PostID;
                        post.ParentCommentID=post.ParentCommentID;
                        post.AuthorID = user.UserID;
                        post.Text =post.Text;
                        post.TimePosted = DateTime.Now;
                        int result = PostCommentManager.Insert(post);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Title = "Create";
                        ViewBag.Error = ex.Message;
                        return View(post);
                    }



                }
                else
                {
                    TempData["error"] = "Need to be logged in to Post";
                    return RedirectToAction("Index");
                    //return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
                }




                
            }
            
        }
   
        private User GetLoggedInUser()
        {
            return HttpContext.Session.GetObject<User>("user");
        }
    }
}





    

