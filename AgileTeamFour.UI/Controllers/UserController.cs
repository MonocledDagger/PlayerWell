using AgileTeamFour.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

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
            return View(UserManager.LoadById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            //UserVM userVM = new UserVM();
            //userVM.User=new BL.Models.User();
            
            return View();
        }


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(User user)
        //{
        //    try
        //    {
        //        ViewBag.Title = "Create a User";
        //        int result = UserManager.Insert(user);
        //        return RedirectToAction(nameof(Index));
        //    }

        //    catch (Exception ex)
        //    {
        //        ViewBag.Title = "Create";
        //        ViewBag.Error = ex.Message;
        //        return View();

        //    }
        //}



        [HttpPost]
        public IActionResult Create(UserVM userVM)
        {
            try
            {
                ViewBag.Title = "Create a User";

                // Process the image if uploaded
                if (userVM.File != null)
                {
                    // Set the IconPic field to the file's name
                    userVM.User.IconPic = userVM.File.FileName;

                    // Define the path where the image will be saved
                    string path = _host.WebRootPath + "\\images\\";

                    // Save the file to the specified path
                    using (var stream = System.IO.File.Create(path + userVM.File.FileName))
                    {
                        userVM.File.CopyTo(stream);
                        ViewBag.Message = "File uploaded successfully.";
                    }
                }

                // Insert the user data into the database
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


        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Edit a User";
            return View(UserManager.LoadById(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, User user, bool rollback = false)
        {
            try
            {
                int result = UserManager.Update(user, rollback);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

    }
}
