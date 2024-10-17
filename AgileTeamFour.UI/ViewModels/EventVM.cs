using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgileTeamFour.UI.ViewModels
{
    public class EventVM
    {
        public Events Event { get; set; }
        

        public Game Game { get; set; }
        

        public EventVM()
        {
            Game = new Game();
            Game.Platform = "None";
        }

        
    }
}