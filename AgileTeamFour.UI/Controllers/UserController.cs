using AgileTeamFour.UI.Models;
using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;

namespace AgileTeamFour.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public UserController(IWebHostEnvironment host)
        {
            _host = host;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "List of All Users";

            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                // Show the full view
                return View(UserManager.Load());
            }
            else
            {
                //Show a view with some fields missing              
                return RedirectToAction("Index2", "User");
            }

        }
        public IActionResult Index2()
        {
            ViewBag.Title = "List of All Users";

            //Helps to determine which Edit, Details, and Delete links to display in view for those that are not admins
            TempData["UserID"] = Authenticate.GetUserID(HttpContext);

            return View(UserManager.Load());
        }

        private void SetUser(User user)
        {
            HttpContext.Session.SetObject("user", user);

            if (user != null)
            {
                HttpContext.Session.SetObject("username", "Welcome " + user.UserName);
            }
            else
            {
                HttpContext.Session.SetObject("username", string.Empty);
            }
        }

        public IActionResult Logout()
        {
            SetUser(null);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                bool result = UserManager.Login(user);
                SetUser(user);

                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        public IActionResult Details(int id)
        {
            ViewBag.Title = "Details for User";

            if (Authenticate.IsAuthenticated(HttpContext, id) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                // Show the view with all of its details
                return View(UserManager.LoadById(id));
            }
            else
            {
                //Show a view with some fields missing              
                return RedirectToAction("Details2", "User", new { Id = id });
            }
        }
        public IActionResult Details2(int Id)
        {
            TempData["UserID"] = Authenticate.GetUserID(HttpContext);
            UserProfileVM userProfileVM = new UserProfileVM(Id);
            ViewBag.Title = "Details for User";
            return View(userProfileVM);
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(UserVM userVM)
        {
            try
            {
                ViewBag.Title = "Create a User";

                // Check if username is taken already
                if (UserManager.UsernameExists(userVM.User.UserName))
                    ModelState.AddModelError("UserName", "The username already exists. Please choose another.");


                if (userVM.File != null)
                {
                    userVM.User.IconPic = userVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";
                    using (var stream = System.IO.File.Create(path + userVM.File.FileName))
                    {
                        userVM.File.CopyTo(stream);
                        ViewBag.Message = "File uploaded successfully.";
                    }
                }
                userVM.User.AccessLevel = "user";  //default creating of a user should start them the lowest account access level
                int result = UserManager.Insert(userVM.User);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create";
                ViewBag.Error = ex.Message;
                return View(userVM);
            }
        }


        //public IActionResult Edit(int id)
        //{
        //    ViewBag.Title = "Edit a User";
        //    return View(UserManager.LoadById(id));
        //}
        //[HttpPost]
        //public IActionResult Edit(int id, User user, bool rollback = false)
        //{
        //    UserVM userVM=new UserVM();
        //    try
        //    {

        //        int result = UserManager.Update(user, rollback);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //        return View(userVM);
        //    }

        //    //if (Authenticate.IsAuthenticated(HttpContext))
        //    //{
        //    //    ProgramVM programVM = new ProgramVM();

        //    //    programVM.Program = ProgramManager.LoadById(id);
        //    //    programVM.DegreeTypes = DegreeTypeManager.Load();

        //    //    ViewBag.Title = "Edit " + programVM.Program.Description;

        //    //    return View(programVM);
        //    //}
        //    //else
        //    //    return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });




        //}
        public IActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                UserVM userVM = new UserVM();
                userVM.User = UserManager.LoadById(id);
                return View(userVM);
            }
            if (Authenticate.IsAuthenticated(HttpContext, id))
            {
                UserVM userVM = new UserVM();

                userVM.User = UserManager.LoadById(id);
                return RedirectToAction("Edit2", "User", new { Id = id });
            }
            else if (Authenticate.IsAuthenticated(HttpContext))
            {
                TempData["error"] = "Cannot Edit data of other Users.";
                return RedirectToAction("Index2", "User");
            }
            else
            {
                TempData["error"] = "Need to be logged in to Edit User data.";
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, UserVM userVM, bool rollback = false)
        {
            try
            {
                // Process the image

                if (userVM.File != null)
                {
                    userVM.User.IconPic = userVM.File.FileName;

                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + userVM.File.FileName))
                    {
                        userVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }


                int result = UserManager.Update(userVM.User, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(userVM);
            }
        }
        public IActionResult Edit2(int Id)
        {

            if (Authenticate.IsAuthenticated(HttpContext, Id))
            {
                UserVM userVM = new UserVM();

                userVM.User = UserManager.LoadById(Id);
                // userVM.DegreeTypes = DegreeTypeManager.Load();

                //ViewBag.Title = "Edit " + programVM.Program.Description;

                return View(userVM);
            }
            else if (Authenticate.IsAuthenticated(HttpContext))
            {
                TempData["error"] = "Cannot Edit data of other Users.";
                return RedirectToAction("Index2", "User");
            }
            else
            {
                TempData["error"] = "Need to be logged in to Edit User data.";
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }

        }

        [HttpPost]
        public IActionResult Edit2(int id, UserVM userVM, bool rollback = false)
        {
            try
            {
                // Process the image

                if (userVM.File != null)
                {
                    userVM.User.IconPic = userVM.File.FileName;

                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + userVM.File.FileName))
                    {
                        userVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }


                int result = UserManager.Update(userVM.User, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(userVM);
            }
        }

        // Pass in the UserId without passing through the Entire UserProfileVM
        public IActionResult EditPassword(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext, id) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                User user = UserManager.LoadById(id);
                EditPasswordVM viewModel = new EditPasswordVM { CurrentUser = user };
                return View(viewModel);
            }
            else
            {
                TempData["error"] = "You must be logged in to edit your password.";
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult EditPassword(EditPasswordVM viewModel)
        { 
            //Load the User from the id in the viewModel
            viewModel.CurrentUser = UserManager.LoadById(viewModel.CurrentUser.UserID);

            // Validate the passwords again just in case
            if (string.IsNullOrEmpty(viewModel.NewPassword) || string.IsNullOrEmpty(viewModel.ConfirmPassword))
            {
                ViewBag.Error = "Password fields cannot be empty.";
                return View("EditPassword", viewModel);
            }
            if (viewModel.NewPassword != viewModel.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View("EditPassword", viewModel);
            }
            try
            { // Hash the new password using the UserManager's GetHash method
                string hashedPassword = UserManager.GetHash(viewModel.NewPassword);
                viewModel.CurrentUser.Password = hashedPassword;
                // Update the user in the database
                int isUpdated = UserManager.Update(viewModel.CurrentUser);
                if (isUpdated > 0)
                {
                    ViewBag.Message = "Password updated successfully.";
                    return RedirectToAction("Details2", new { id = viewModel.CurrentUser.UserID });
                }
                else
                {
                    ViewBag.Error = "Failed to update the password.";
                    return View("EditPassword", viewModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message; return View("EditPassword", viewModel);
            }
        }

        public IActionResult Deactivate(int Id)
        {
            if (Authenticate.IsAuthenticated(HttpContext, Id) || Authenticate.IsAuthenticated(HttpContext, "admin"))
            {
                return View("Deactivate", UserManager.LoadById(Id));
            }
            else
            {
                return RedirectToAction("Index2", "User");
            }
        }


        [HttpPost, ActionName("Deactivate")]
        public IActionResult DeactivatePost(int Id)
        {
            try
            {
                User user = UserManager.LoadById(Id);
                user.AccessLevel = "deactivated";
                UserManager.Update(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
