using AgileTeamFour.BL.Models;
using AgileTeamFour.PL;
using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;

namespace AgileTeamFour.UI.Controllers
{
    public class FriendController : Controller
    {
        public ActionResult Index()
        {
            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId && e.Status == "Approved" || e.ReceiverID == userId && e.Status == "Approved").OrderBy(e => e.ID);

            List<User> friendsList = new List<User>();
            List<int> friendsIds = new List<int>();
            foreach (Friend vm in friends)
            { 
                User Sender = UserManager.Load().Where(e => e.UserID == vm.SenderID).FirstOrDefault();
                User Receiver = UserManager.Load().Where(e => e.UserID == vm.ReceiverID).FirstOrDefault();

                if (!friendsIds.Contains(Sender.UserID) && Sender.UserID != userId)
                {
                    friendsList.Add(Sender);
                    friendsIds.Add(Sender.UserID);

                }
                if (!friendsIds.Contains(Receiver.UserID) && Receiver.UserID != userId)
                {
                    friendsList.Add(Receiver);
                    friendsIds.Add(Receiver.UserID);
                }
            }

            var friendVMs = new List<FriendVM>();

            friendVMs = friends.Select(e => new FriendVM
            {
                User = user,
                Friend = e,
                UserReceiver = UserManager.LoadById(e.SenderID),
                UserSender = UserManager.LoadById(e.ReceiverID),
                FriendsList = friendsList,
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
            var friendVMs = friends.Select(f => new FriendVM
            {
                Friend = f,
                MyFriends = f.SenderID == userId
        ? new List<User> { UserManager.LoadById(f.ReceiverID) }
        : new List<User> { UserManager.LoadById(f.SenderID) }
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
            var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status == "Approved"
                                                  || e.ReceiverID == userId & e.Status == "Approved");

            // Filter out the distint Ids for your friends
            var friendIds = friends.SelectMany(f => new[] { f.SenderID, f.ReceiverID })
                .Where(id => id != userId).Distinct().ToList();

            FriendVM friendVM = new FriendVM();
            List<User> users = UserManager.Load().Where(u => friendIds.Contains(u.UserID)).ToList();

            ViewBag.Title = "Friends";

            // Pass the filtered list of EventDetailsVM to the view
            return View(users);
        }

        public ActionResult MyPendingIndex()
        {
            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            // Load Friends and filter by UserId
            var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status=="Pending" || e.ReceiverID == userId & e.Status =="Pending");

            var myFriends = friends.Select(f => 
        f.SenderID == userId 
            ? UserManager.LoadById(f.ReceiverID) 
            : UserManager.LoadById(f.SenderID))
        .ToList();
            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserSender = UserManager.LoadById(e.SenderID),
                UserReceiver = UserManager.LoadById(e.ReceiverID),

                MyFriends = e.SenderID == userId
            ? new List<User> { UserManager.LoadById(e.ReceiverID) }
            : new List<User> { UserManager.LoadById(e.SenderID) }

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

            //var friends = FriendManager.Load().Where(e => e.SenderID == userId & e.Status == "Blocked" || e.ReceiverID == userId & e.Status == "Blocked");
            
            //Should only display people USER has blocked, not who has blocked user
            var friends = FriendManager.Load().Where(e => e.ReceiverID == userId & e.Status == "Blocked");



            // Map each filtered event to the EventDetailsVM
            var friendVMs = friends.Select(e => new FriendVM
            {
                Friend = e,
                UserReceiver = UserManager.LoadById(e.SenderID),
                UserSender = UserManager.LoadById(e.ReceiverID),
                MyFriends = e.SenderID == userId
            ? new List<User> { UserManager.LoadById(e.ReceiverID) }
            : new List<User> { UserManager.LoadById(e.SenderID) }

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

            // Load current user's friends, pending, and blocked by to exclude from list including self
            friendVM.Users = UserManager.Load();

            // Load all friend related entries
            List<Friend> allFriends = FriendManager.Load();

            // Find any entries that have to do with the current user
            var sendMatches = allFriends.Where(f => f.SenderID == userId).Select(f => f.ReceiverID).ToList();
            var receiveMatches = allFriends.Where(f => f.ReceiverID == userId).Select(f => f.SenderID).ToList();

            // Combine them together for simplicity
            var combinedMatches = sendMatches.Concat(receiveMatches).Distinct().ToList();
            combinedMatches.Add(userId); // Add yourself to the list of excluded users

            // Filter the list to only Users that can recieve a friend request
            friendVM.Users = friendVM.Users.Where(u => !combinedMatches.Contains(u.UserID)).ToList();

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

                using (var dc = new AgileTeamFourEntities())
                {

                    // Check if there is an existing friendship
                    var existingRelationship = dc.tblFriends
                    .FirstOrDefault(f =>
                    (f.SenderID == friendVM.Friend.SenderID && f.ReceiverID == friendVM.Friend.ReceiverID) ||
                    (f.SenderID == friendVM.Friend.ReceiverID && f.ReceiverID == friendVM.Friend.SenderID)
                        );

                    // If a relationship exists, return 0 to indicate no new insert
                    if (existingRelationship != null)
                    {
                        TempData["error"] = "Relationship Already Exists";
                        return RedirectToAction("MyIndex", "Friend");
                    }
                    else if (friendVM.Friend.SenderID == friendVM.Friend.ReceiverID) //Check if Friending self
                    {
                        TempData["error"] = "You Cannot Friend Yourself";
                        return RedirectToAction("MyIndex", "Friend");
                    }
                }
                // Insert the Friend request into the database
                int result = FriendManager.Insert(friendVM.Friend);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult CreateBlock()
        {
            ViewBag.Title = "Friend Request";

            FriendVM friendVM = new FriendVM();

            // Get userID from session
            var user = HttpContext.Session.GetObject<User>("user");
            var userId = user?.UserID ?? 0;

            friendVM.Friend = new BL.Models.Friend();

            // Load current user's friends, pending, and blocked by to exclude from list including self
            friendVM.Users = UserManager.Load();

            // Load all friend related entries
            List<Friend> allFriends = FriendManager.Load();

            // Find any entries that have to do with the current user
            var sendMatches = allFriends.Where(f => f.SenderID == userId).Select(f => f.ReceiverID).ToList();
            var receiveMatches = allFriends.Where(f => f.ReceiverID == userId).Select(f => f.SenderID).ToList();

            // Combine them together for simplicity
            var combinedMatches = sendMatches.Concat(receiveMatches).Distinct().ToList();
            combinedMatches.Add(userId); // Add yourself to the list of excluded users

            // Filter the list to only Users that can recieve a friend request
            friendVM.Users = friendVM.Users.Where(u => !combinedMatches.Contains(u.UserID)).ToList();

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
        public IActionResult CreateBlock(FriendVM friendVM)
        {
            try
            {
                // Get the SenderID from session (the logged-in user)
                var user = HttpContext.Session.GetObject<User>("user");
                var recieverID = user?.UserID ?? 0;

                // Ensure the session user is the sender, and the form provides the receiver

                //ReceiverID and SenderID are opposite of the Create function
                friendVM.Friend.ReceiverID = recieverID;
                friendVM.Friend.SenderID = friendVM.Friend.SenderID; // Set from form input
                friendVM.Friend.Status = "Blocked";

                using (var dc = new AgileTeamFourEntities())
                {

                    // Check if there is an existing friendship
                    var existingRelationship = dc.tblFriends
                    .FirstOrDefault(f =>
                    (f.SenderID == friendVM.Friend.SenderID && f.ReceiverID == friendVM.Friend.ReceiverID) ||
                    (f.SenderID == friendVM.Friend.ReceiverID && f.ReceiverID == friendVM.Friend.SenderID)
                        );

                    //If a relationship exists, return 0 to indicate no new insert
                    if (existingRelationship != null)
                    {
                        TempData["error"] = "Relationship Already Exists";
                        return RedirectToAction("MyIndex", "Friend");
                    }
                    else if (friendVM.Friend.ReceiverID == friendVM.Friend.SenderID) //Check if Friending self
                    {
                        TempData["error"] = "You Cannot Block Yourself";
                        return RedirectToAction("MyIndex", "Friend");
                    }
                }
                // Insert the Friend request into the database
                int result = FriendManager.Insert(friendVM.Friend);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult AcceptRequest(int friendId)
        {
            FriendManager.AcceptFriendRequest(friendId);
            return RedirectToAction("MyApprovedIndex");
        }
        [HttpPost]
        public IActionResult RejectRequest(int friendId)
        {
            FriendManager.Delete(friendId);
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

