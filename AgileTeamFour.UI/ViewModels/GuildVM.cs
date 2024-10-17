namespace AgileTeamFour.UI.ViewModels
{
    public class GuildVM
    {
        public Guild Guild { get; set; }

        //Game = user
        public List<User> Users { get; set; } = new List<User>();


        public GuildVM()
        {
            Users = UserManager.Load();
            //Game.Platform = "None";
        }
    }
}
