using AgileTeamFour.BL.Models;

namespace AgileTeamFour.UI.Models
{
    public static class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            User user = context.Session.GetObject<User>("user");
            if (user != null && user.AccessLevel.ToLower() != "deactivated")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsAuthenticated(HttpContext context, string Access)
        {
            User user = context.Session.GetObject<User>("user");
            if (user != null && user.AccessLevel.ToLower() == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool IsAuthenticated(HttpContext context, int matchingId)
        {
            User user = context.Session.GetObject<User>("user");
            if(user != null && user.UserID == matchingId && user.AccessLevel.ToLower() != "deactivated")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int GetUserID(HttpContext context)
        {
            User user = context.Session.GetObject<User>("user");
            if (user != null)
            {
                return user.UserID;
            }
            else
            {
                return 0;
            }
        }
        public static string GetUserName(HttpContext context)
        {
            User user = context.Session.GetObject<User>("user");
            if (user != null)
            {
                return user.UserName;
            }
            else
            {
                return "";
            }
        }
        public static bool IsAdmin(HttpContext context)
        {
            User user = context.Session.GetObject<User>("user");
            if (user != null && user.AccessLevel.ToLower() == "admin")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
