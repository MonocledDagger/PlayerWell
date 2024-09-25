using AgileTeamFour.BL.Models;

namespace AgileTeamFour.UI.ViewModels
{
    public class EventDetailsVM
    {
        public Events Event { get; set; }
        public Game Game { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public IEnumerable<PlayerEvent> PlayerEvents { get; set; }
        public List<Comment> Comments { get; set; }
    }

}
