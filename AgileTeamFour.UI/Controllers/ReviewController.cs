using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;

namespace AgileTeamFour.Web.Controllers
{
    public class ReviewController : Controller
    {
        private User GetLoggedInUser()
        {
            return HttpContext.Session.GetObject<User>("user");
        }

        public ActionResult Index()
        {
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {   // Case for admin gong to Reviews Index
                ViewBag.Title = "List of Reviews";
                List<Review> items = ReviewManager.Load();
                return View(items);
            }  
            else if (Authenticate.IsAuthenticated(HttpContext))
            {  // Case for 
                ViewBag.Title = "Your Reviews";
                User user = GetLoggedInUser();
                List<Review> reviews = ReviewManager.LoadPlayerReviews(user.UserID).Where(r => r.ReviewText == "87|6#x4A|tkg").ToList();
                return View("Players", reviews);
            }
            else
            {
                TempData["error"] = "Need admin rights to view page";
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        public ActionResult Details(int id)
        {
            return View(ReviewManager.LoadById(id));
        }

        public ActionResult Create(int authorId, int recipientId)
        {
            // Retrieve the logged-in user from session
            var loggedInUser = GetLoggedInUser();

            // Check if the logged-in user's ID matches the AuthorID
            if (loggedInUser == null || loggedInUser.UserID != authorId)
            {
                // If not authorized, redirect to an unauthorized or another page
                return RedirectToAction("Index", "Home");
            }

            else
            {
                // Create a new Review model with the provided AuthorID and RecipientID
                var model = new Review
                {
                    AuthorID = authorId,
                    RecipientID = recipientId
                };

                ViewBag.Title = "Create a Review";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Review review)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ReviewManager.Insert(review);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var items = ReviewManager.LoadById(id);

            ViewBag.Title = "Edit ";
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                return View(items);
            }
            else
            {
                TempData["error"] = "Need admin rights to view page";
                return RedirectToAction("Index", "Review");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Review review, bool rollback = false)
        {

            try
            {
                int result = ReviewManager.Update(review, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(review);
            }
        }

        public ActionResult Delete(int id)
        {
            Review review = ReviewManager.LoadById(id);
            if (review == null)
            {
                return NotFound();
            }
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                return View(review);
            }
            else
            {
                TempData["error"] = "Need admin rights to view page";
                return RedirectToAction("Index", "Review");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ReviewManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public void GenerateReviews()
        {
            try
            {
                ReviewManager.CreatePlayerReviewsAfterEvent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating reviews: {ex.Message}"); ;
            }
        }

        public void RemoveReviews()
        {
            try
            {
                ReviewManager.DeleteIncompleteReviews();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing inactive reviews: {ex.Message}");
            }
        }

        public ActionResult Complete(int id)
        {
            Review review = ReviewManager.LoadById(id);
            review.ReviewText = "";

            ViewBag.Title = "Complete Review";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(review);
            }
            else
            {
                TempData["error"] = "Login to complete reviews.";
                return RedirectToAction("Index", "Review");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }
    }
}
