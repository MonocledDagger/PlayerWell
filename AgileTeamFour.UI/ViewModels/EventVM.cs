using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgileTeamFour.UI.ViewModels
{
    public class EventVM
    {
        public Events Event { get; set; }
        //public Game Game { get; set; } = new Game();
        //public IEnumerable<PlayerEvent> PlayerEvents { get; set; } = new List<PlayerEvent>();
        //public List<Comment> Comments { get; set; } = new List<Comment>();

        public Game Game { get; set; }
        //public IEnumerable<int> GameIDs { get; set; } = new List<int>();

        public EventVM()
        {
            Game = new Game();
            Game.Platform = "None";
        }

        //public EventVM(int id)
        //{
            
        //    Games = GameManager.Load();
            
        //    Event = EventManager.LoadByID(id);
        //    GameIDs = Event.Games.Select(g => g.GameID);
        //    PlayerEvents = PlayerEventManager.LoadByEventID(id);
        //    Comments = CommentManager.LoadByEventID(id);

        //    //Game = GameManager.LoadByID(id);
        //}
    }
}