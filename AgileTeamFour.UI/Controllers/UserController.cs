using AgileTeamFour.BL.Models;
using AgileTeamFour.UI.Models;
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
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserVM userVM)
        {
            try
            {
                ViewBag.Title = "Create a User";

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
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                UserVM userVM = new UserVM();

                userVM.User = UserManager.LoadById(id);
               // userVM.DegreeTypes = DegreeTypeManager.Load();

                //ViewBag.Title = "Edit " + programVM.Program.Description;

                return View(userVM);
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
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

    }
}
