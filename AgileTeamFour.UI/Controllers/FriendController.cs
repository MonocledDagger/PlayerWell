using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Linq;

namespace AgileTeamFour.UI.Controllers
{
    public class FriendController : Controller
    {
        public ActionResult Index()
        {
            /*
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
            */
            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status == "Approved" || e.ReceiverID == userId & e.Status == "Approved");

            List<User> friendsList = new List<User>();
            foreach(Friend vm in friends)
            {
                User friendUser = UserManager.Load().Where(e => e.UserID == vm.ID).FirstOrDefault();
                User Sender = UserManager.Load().Where(e => e.UserID == vm.SenderID).FirstOrDefault();
                User Receiver = UserManager.Load().Where(e => e.UserID == vm.ReceiverID).FirstOrDefault();

                if (!friendsList.Contains(friendUser) && vm.ID != userId)
                {
                    friendsList.Add(friendUser);
;               }
                if (!friendsList.Contains(Sender) && vm.SenderID != userId)
                {
                    friendsList.Add(Sender);
                    
                }
                if (!friendsList.Contains(Receiver) && vm.ReceiverID != userId)
                {
                    friendsList.Add(Receiver);
                    
                }
            }

            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserReceiver = UserManager.LoadById(e.SenderID),
                UserSender = UserManager.LoadById(e.ReceiverID),
                FriendsList = friendsList

            }).ToList();

            ViewBag.Title = "Friends";

            // Pass the filtered list of EventDetailsVM to the view
            return View(friendVMs);
        }

        public ActionResult MyIndex()
        {
            // Get userID from session
               var user = HttpContext.Session.GetObject<User>("user");
               var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId || e.ReceiverID ==userId) ;

            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserReceiver = UserManager.LoadById(e.SenderID),
                UserSender = UserManager.LoadById(e.ReceiverID),

            }).ToList();

            ViewBag.Title = "Friends";

            // Pass the filtered list of EventDetailsVM to the view
            return View(friendVMs);
        }

        public ActionResult MyApprovedIndex()
        {
            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status == "Approved" || e.ReceiverID == userId & e.Status == "Approved");

            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserReceiver = UserManager.LoadById(e.SenderID),
                UserSender = UserManager.LoadById(e.ReceiverID),

            }).ToList();

            ViewBag.Title = "Friends";

            // Pass the filtered list of EventDetailsVM to the view
            return View(friendVMs);
        }

        public ActionResult MyPendingIndex()
        {
            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status=="Pending" || e.ReceiverID == userId & e.Status =="Pending");

            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserSender = UserManager.LoadById(e.SenderID),
                UserReceiver = UserManager.LoadById(e.ReceiverID),

            }).ToList();

            ViewBag.Title = "Friends";

            //Pass userId to view to limit who can accept a friend request

            ViewData["userId"] = userId;
            // Pass the filtered list of friends to the view
            return View(friendVMs);
        }

        public ActionResult MyBlockedIndex()
        {
            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status == "Blocked" || e.ReceiverID == userId & e.Status == "Blocked");

            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserReceiver = UserManager.LoadById(e.SenderID),
                UserSender = UserManager.LoadById(e.ReceiverID),

            }).ToList();

            ViewBag.Title = "Friends";

            // Pass the filtered list of EventDetailsVM to the view
            return View(friendVMs);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Friend Request";

            FriendVM friendVM = new FriendVM();

            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            friendVM.Friend = new BL.Models.Friend();
            friendVM.Users = UserManager.Load();

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                TempData["userId"] = userId;
                return View(friendVM);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        [HttpPost]
        public IActionResult Create(FriendVM friendVM)
        {
            try
            {
                // Get the SenderID from session (the logged-in user)
                var user = HttpContext.Session.GetObject<User>("user");
                var senderID = user?.UserID ?? 0;

                // Ensure the session user is the sender, and the form provides the receiver
                friendVM.Friend.SenderID = senderID;
                friendVM.Friend.ReceiverID = friendVM.Friend.ReceiverID; // Set from form input
                friendVM.Friend.Status = "Pending";

                // Insert the Friend request into the database
                int result = FriendManager.Insert(friendVM.Friend);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpPost]
        //public IActionResult SendRequest(int receiverId)
        //{
        //    // Get the user from session
        //    var user = HttpContext.Session.GetObject<User>("user");

        //    // Ensure the user is logged in
        //    if (user == null)
        //    {

        //        return RedirectToAction("Login");
        //    }

        //    // Sender ID is the current user's ID
        //    var senderId = user.UserID;


        //    FriendManager.Insert(senderId, receiverId);

        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public IActionResult AcceptRequest(int friendId)
        {
            FriendManager.AcceptFriendRequest(friendId);
            return RedirectToAction("MyApprovedIndex");
        }
        [HttpPost]
        public IActionResult RejectRequest(int friendId)
        {
            FriendManager.RejectFriendRequest(friendId);
            return RedirectToAction("MyIndex");
        }
        [HttpPost]
        public IActionResult BlockFriend(int friendId)
        {
            FriendManager.BlockPlayer(friendId);
            return RedirectToAction("MyBlockedIndex");
        }
    }
}

