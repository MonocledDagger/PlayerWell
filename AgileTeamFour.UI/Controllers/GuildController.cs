using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgileTeamFour.UI.Controllers
{
    public class GuildController : Controller
    {
        //Event = Guild
        //Game = User
        public ActionResult Index()
        {
            var guilds = GuildManager.Load(); // Load the guilds from the manager

            // Map each Guilds object to GuildDetailsVM
            var guildDetailsVMs = guilds.Select(e => new GuildDetailsVM
            {
                Guild = e, // Assign the guild object
                User = UserManager.LoadById(e.GuildId) // Load User object for each guild
            }).ToList();

            ViewBag.Title = "List of Guilds";

            // Pass the list of GuildDetailsVM to the view
            return View(guildDetailsVMs);
        }

        public ActionResult Index2()
        {
            var guilds = GuildManager.Load(); // Load the guilds from the manager

            // Map each Guilds object to GuildDetailsVM
            var guildDetailsVMs = guilds.Select(e => new GuildDetailsVM
            {
                Guild = e, // Assign the guild object
                User = UserManager.LoadById(e.GuildId) // Load the corresponding User object for each guild
            }).ToList();

            ViewBag.Title = "List of Guilds";

            // Pass the list of GuildDetailsVM to the view
            return View(guildDetailsVMs);
        }



        public ActionResult Details(int id)
        {
            var guildItem = GuildManager.LoadByID(id);
            if (guildItem == null)
            {
                return NotFound();
            }

            //Get PlayerID from session for sign up
            var Currentuser = HttpContext.Session.GetObject<User>("user");
            var playerID = Currentuser?.UserID ?? 0;

            var user = UserManager.LoadById(guildItem.GuildId);
            var playerGuilds = PlayerGuildManager.LoadByGuildID(id);
            //var comments = CommentManager.LoadByGuildID(id);


            // Count the number of players signed up
            int currentPlayers = playerGuilds != null ? playerGuilds.Count() : 0;

            // Create the ViewModel
            var guildDetailsVM = new GuildDetailsVM
            {
                Guild = guildItem,
                User = user,
                PlayerGuilds = playerGuilds ?? new List<PlayerGuild>(),
                //Comments = comments ?? new List<Comment>(),
                PlayerID = playerID,
                currentPlayers = currentPlayers,
                LeaderName = GuildManager.GetLeaderName(id),
                Users = UserManager.Load() ?? new List<User>()
            };


            ViewBag.Title = "Details for " + guildItem.GuildName;



            // Pass the ViewModel to the view
            return View(guildDetailsVM);
        }

        public ActionResult Create()
        {
            GuildVM vm = new GuildVM();
            ViewBag.Title = "Create an Guild";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                var Currentuser = HttpContext.Session.GetObject<User>("user");
                int playerID = Currentuser.UserID;
                TempData["authorID"] = Currentuser.UserID;
                return View(vm);
            }
            else
            {
                TempData["error"] = "Need to be logged in to Create an Guild.";
                return RedirectToAction("Index", "Guild");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GuildVM guildCreateVM)
        {   // Check if user already exists in database if not add it
            //User existingUser = UserManager.LoadById(guildCreateVM.Guild.GuildId);

            //if (existingUser == null)
            //{
            //    User newUser = new User
            //    {
            //        UserID = guildCreateVM.Guild.GuildId,
            //        UserName = guildCreateVM.User.UserName,
                    
            //    };

            //    UserManager.Insert(newUser);
            //}

            try
            {
                if (ModelState.IsValid)
                {
                    int guildID = 0;
                    int authorID = 1;
                    guildCreateVM.Guild.LeaderId = (int)TempData["authorID"];
                    GuildManager.Insert(ref guildID,
                        
                        guildCreateVM.Guild.GuildName,
                        
                        guildCreateVM.Guild.Description,
                        guildCreateVM.Guild.LeaderId
                        
                        );
                    return RedirectToAction(nameof(Index));
                }
                return View(guildCreateVM);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.InnerException.Message;
                return RedirectToAction("Index", "Guild");
                //return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var guildItem = GuildManager.LoadByID(id);
            if (guildItem == null)
            {
                return NotFound();
            }

            // Create the ViewModel
            var guildDetailsVM = new GuildVM
            {
                Guild = guildItem,
                User = UserManager.LoadById(guildItem.GuildId),
            };

            ViewBag.Title = "Edit " + guildItem.GuildName;

            if (Authenticate.IsAuthenticated(HttpContext, guildItem.LeaderId) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                // Pass the ViewModel to the view
                return View(guildDetailsVM);
            }
            else
            {
                TempData["error"] = "Need admin or author rights to view page";
                return RedirectToAction("Index", "Guild");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GuildVM model)
        {
            if (ModelState.IsValid)
            {
                // Update the guild using the data from the ViewModel
                var guildItem = GuildManager.LoadByID(model.Guild.GuildId);
                if (guildItem == null)
                {
                    return NotFound();
                }

                // Update the guild fields
                guildItem.GuildName = model.Guild.GuildName;
                guildItem.GuildId = model.Guild.GuildId;
                guildItem.Description = model.Guild.Description;

                // Save changes
                GuildManager.Update(guildItem);

                return RedirectToAction("Index");
            }

            else
            {
                return View(model);
            }

        }


        public ActionResult Delete(int id)
        {
            var guildItem = GuildManager.LoadByID(id);
            if (guildItem == null)
            {
                return NotFound();
            }
            var user = UserManager.LoadById(guildItem.GuildId);


            // Create the ViewModel
            var guildDetailsVM = new GuildDetailsVM
            {
                Guild = guildItem,
                User = user,
                LeaderName = GuildManager.GetLeaderName(id)

            };

            ViewBag.Title = "Delete " + guildItem.GuildName;

            if (Authenticate.IsAuthenticated(HttpContext, guildItem.LeaderId) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                // Pass the ViewModel to the view
                return View(guildDetailsVM);
            }
            else
            {
                TempData["error"] = "Need admin or author rights to view page";
                return RedirectToAction("Index", "Guild");//RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                GuildManager.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GuildsForUser(int userID)
        {
            // Load all guilds and filter by the provided UserID
            var guilds = GuildManager.Load().Where(e => e.GuildId == userID);

            // Map each filtered guild to the GuildDetailsVM
            var guildDetailsVMs = guilds.Select(e => new GuildDetailsVM
            {
                Guild = e,
                User = UserManager.LoadById(e.GuildId)
            }).ToList();

            ViewBag.Title = $"Guilds for User ID: {userID}";

            // Pass the filtered list of GuildDetailsVM to the view
            return View(guildDetailsVMs);
        }

        [HttpPost]
        public ActionResult SignUp(int GuildID, int PlayerID, string Role)
        {
            try
            {
                // Check if the player is already signed up for this guild
                var playerList = PlayerGuildManager.LoadByGuildID(GuildID);
                var existingPlayer = playerList.FirstOrDefault(pe => pe.PlayerID == PlayerID);


                //PlayerID is gotten from session when the page loads. See ActionResult Details.
                if (PlayerID == 0)
                {
                    //If User isn't signed in
                    TempData["error"] = "Please Sign In Before Joining an Guild";
                    // Redirect back to the details page
                    return RedirectToAction("Details", new { id = GuildID });


                }
                else if (existingPlayer != null)
                {
                    // Player is already signed up for the guild
                    TempData["error"] = "You are already signed up for this guild.";
                    return RedirectToAction("Details", new { id = GuildID });

                }
                else
                {
                    // Insert the new player guild
                    PlayerGuildManager.Insert(PlayerID, GuildID, Role);


                    // Redirect back to the details page
                    return RedirectToAction("Details", new { id = GuildID });
                }

            }
            catch (Exception ex)
            {
                // Show error message 
                ViewBag.ErrorMessage = "Error while signing up: " + ex.Message;
                return RedirectToAction("Details", new { id = GuildID });
            }
        }


        public ActionResult LeaveGuild(int guildID, int playerID)
        {


            // Perform the deletion only if the player is signed in
            if (playerID != 0)
            {
                PlayerGuildManager.Delete(playerID, guildID); // Delete by GuildID and PlayerID
            }
            //return RedirectToAction("Details", new { id = id }); // Redirect back to guild details
            return RedirectToAction("Details", new { id = guildID });
        }

        //[HttpPost]
        //public ActionResult AddComment(string CommentText, int guildID, int playerID)
        //{
        //    Comment comment = new Comment();
        //    comment.TimePosted = DateTime.Now;
        //    comment.LeaderID = playerID;
        //    comment.GameID = guildID;
        //    comment.Text = CommentText;

        //    if (CommentText != null && CommentText.Trim() != "")
        //    {
        //        CommentManager.Insert(comment);
        //    }


        //    return RedirectToAction("Details", new { id = guildID });
        //}
    }
}
