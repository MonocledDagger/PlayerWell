using AgileTeamFour.BL.Models;
using System.ComponentModel;

namespace AgileTeamFour.UI.ViewModels
{
    public class EventDetailsVM
    {
        public Events Event { get; set; }
        public Game Game { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public IEnumerable<PlayerEvent> PlayerEvents { get; set; }
        public List<Comment> Comments { get; set; }
        public List<User> Users { get; set; }
        public int PlayerID { get; set; }
        [DisplayName("Event Name")]
        public int currentPlayers {  get; set; }

        [DisplayName("Event Creator")]
        public string AuthorName { get; set; }
    }

}
