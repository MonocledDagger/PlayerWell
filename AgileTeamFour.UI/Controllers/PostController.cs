
using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.Web.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Index()
        {
            
            ViewBag.ErrorMessage = TempData["error"];
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
        public IActionResult CreateC()
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
                        if (post.Text == null)
                        {
                            TempData["error"] = "Text cannot be empty";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            User user = GetLoggedInUser();
                            ViewBag.Title = "post";
                            post.PostID = post.PostID;
                            post.ParentCommentID = post.ParentCommentID;
                            post.AuthorID = user.UserID;
                            post.Text = post.Text;
                            post.TimePosted = DateTime.Now;
                            int result = PostCommentManager.Insert(post);
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Title = "Index";
                        ViewBag.Error = ex.Message;
                        return View(post);
                    }

                }
                else
                {
                    TempData["error"] = "Need to be logged in to Post";
                    return RedirectToAction("Index");
                }
     
            }
            
        }
        [HttpPost]

        public IActionResult DeleteReply(int replyId)
        {
            using (var dc = new AgileTeamFourEntities())
            {
                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    try
                    {
                        var reply = dc.tblPostComments.FirstOrDefault(c => c.CommentID == replyId);

                        if (reply == null) 
                        {
                            TempData["error"] = "Reply not found.";
                            return RedirectToAction("Index");
                        }

                        User user = GetLoggedInUser();

                        if (reply.AuthorID != user.UserID) 
                        {
                            TempData["error"] = "You are not authorized to delete this reply.";
                            return RedirectToAction("Index");
                        }

                        dc.tblPostComments.Remove(reply);
                        dc.SaveChanges();

                        TempData["success"] = "Reply deleted successfully.";
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = $"An error occurred: {ex.Message}";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["error"] = "You need to be logged in to delete replies.";
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        public IActionResult CreateC(Post post)
        {
            using (var dc = new AgileTeamFourEntities())
            {

                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    try
                    {
                        User user = GetLoggedInUser();
                        ViewBag.Title = "post";
                        post.PostID = post.PostID;
                       
                        post.AuthorID = user.UserID;
                        post.Text = post.Text;
                        post.TimePosted = DateTime.Now;
                        post.Image ="here goes the image";
                        int result = PostManager.Insert(post);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Title = "Index";
                        ViewBag.Error = ex.Message;
                        return View(post);
                    }



                }
                else
                {
                    TempData["error"] = "Need to be logged in to Post";
                    return RedirectToAction("Index");
                 }

            }

        }



        [HttpPost]
        public IActionResult ToggleLike([FromBody] ToggleLikeRequest request)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                try
                {
                    User user = GetLoggedInUser();
                    int userId = user.UserID;
                    var isLiked = PostManager.toggleLike(request.PostId, userId);
                    return Json(new { success = true, isLiked });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, error = ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, error = "Need to be logged in to Like a Post" });
            }
        }


        [HttpGet]
        public IActionResult GetLikes(int postId)
        {
            var likes = PostManager.GetPostLikes(postId);
            return Json(likes);
        }

        private User GetLoggedInUser()
        {
            return HttpContext.Session.GetObject<User>("user");
        }
    }
}





    

