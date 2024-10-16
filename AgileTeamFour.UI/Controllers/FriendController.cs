using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgileTeamFour.UI.Controllers
{
    public class FriendController : Controller
    {
        public ActionResult Index()
        {
            var guilds = FriendManager.Load(); // Load the guilds from the manager


            // Map each Guilds object 
            var FriendVM = guilds.Select(e => new FriendVM
            {
                Friend = e, 
                //User = UserManager.LoadById(e.LeaderId), 

            }).ToList();

            ViewBag.Title = "List of Guilds";

            // Pass the list of GuildDetailsVM to the view
            return View(FriendVM);
        }

        //public IActionResult Index()
        //{
        //    // Get userID from session
        //    var user = HttpContext.Session.GetObject<User>("user");
        //    var userId = user?.UserID ?? 0;

        //    // Get friends and pending requests
        //    var friends = FriendManager.GetFriendsForUser(userId);
        //    var friendRequests = FriendManager.GetPendingRequestsForUser(userId);

        //    // Create the ViewModel
        //    var viewModel = new FriendVM
        //    {
        //        Friends = friends.Select(f => new FriendVM
        //        {
        //            Friend = f,
        //            User = f.ReceiverID == userId ? f.Sender : f.Receiver
        //        }),
        //        PendingRequests = friendRequests.Select(f => new FriendVM
        //        {
        //            Friend = f,
        //            User = f.Sender
        //        })
        //    };

        //    return View(viewModel);
        //}


        public IActionResult SendRequest(int receiverId)
        {
            // Get the user from session
            var user = HttpContext.Session.GetObject<User>("user");

            // Ensure the user is logged in
            if (user == null)
            {
               
                return RedirectToAction("Login");
            }

            // Sender ID is the current user's ID
            var senderId = user.UserID;

            
            FriendManager.Insert(senderId, receiverId);

            return RedirectToAction("Index");
        }

        public IActionResult AcceptRequest(int friendId)
        {
            FriendManager.AcceptFriendRequest(friendId);
            return RedirectToAction("Index");
        }

        public IActionResult BlockFriend(int friendId)
        {
            FriendManager.BlockPlayer(friendId);
            return RedirectToAction("Index");
        }
    }
}

