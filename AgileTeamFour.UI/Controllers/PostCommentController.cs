
using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AgileTeamFour.Web.Controllers
{
    public class PostCommentController : Controller
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
        public IActionResult Create(int postID, string text, int? parentCommentID)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return BadRequest("Comment cannot be empty.");
            }
            User user = GetLoggedInUser();
            var newComment = new PostComment
            {
                
                PostID = postID,
                Text = text,
                ParentCommentID =0,
                TimePosted = DateTime.Now,
                AuthorID = user.UserID, 
            };
            int result = PostCommentManager.Insert(newComment);
            
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult CreateReply([FromBody] PostComment reply)
        {
            try
            {
                if (Authenticate.IsAuthenticated(HttpContext))
                {
                    User user = GetLoggedInUser();
                    reply.AuthorID = user.UserID;
                    reply.TimePosted = DateTime.Now;

                    if (string.IsNullOrWhiteSpace(reply.Text))
                    {
                        TempData["error"] = "Reply cannot be empty.";
                        return RedirectToAction("Index");
                    }

                    int result = PostCommentManager.Insert(reply);

                    if (result > 0)
                    {
                        
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "Failed to save the reply.";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["error"] = "Need to be logged in to Post";
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Internal server error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }


        private User GetLoggedInUser()
            {
                return HttpContext.Session.GetObject<User>("user");
            }
        }
    }




