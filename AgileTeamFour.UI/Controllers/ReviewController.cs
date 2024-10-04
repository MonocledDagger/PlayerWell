using AgileTeamFour.BL;
using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Collections.Generic;
using System.Xml.Linq;

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
            ViewBag.Title = "List of Reviews";
            List<Review> items = ReviewManager.Load();

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
                    return RedirectToAction(nameof(Index));
                }
                return View(review);
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

        public ActionResult Generate(int id)
        {
            try
            {
                ReviewManager.CreatePlayerReviewsAfterEvent(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Remove(int id)
        {
            try
            {
                ReviewManager.DeleteIncompleteReviews(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
