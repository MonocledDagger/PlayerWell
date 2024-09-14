

using AgileTeamFour.BL.Models;

namespace AgileTeamFour.UI.ViewModels
{
    public class EventDetailsVM
    {
        public Events Event { get; set; }
        public Game Game { get; set; }
        public List<PlayerEvent> PlayerEvents { get; set; }
        public List<Comment> Comments { get; set; }
    }

}
