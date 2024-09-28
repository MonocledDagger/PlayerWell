namespace AgileTeamFour.UI.Models
{
    public static class Authenticate
    {
        public static bool IsAuthenticated(HttpContext context)
        {
            if(context.Session.GetObject<User>("user") != null)
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
            if(user != null && user.UserID == matchingId)
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
