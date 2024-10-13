namespace AgileTeamFour.UI.ViewModels
{
    public class GuildVM
    {
        public Guild Guild { get; set; }

        //Game = user
        public User User { get; set; }


        public GuildVM()
        {
            User = new User();
            //Game.Platform = "None";
        }
    }
}
