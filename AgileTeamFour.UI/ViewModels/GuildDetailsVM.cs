using System.ComponentModel;

namespace AgileTeamFour.UI.ViewModels
{
    public class GuildDetailsVM
    {
        //Event = Guild
        //Game=User
        public Guild Guild { get; set; }
        public User User { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public IEnumerable<PlayerGuild> PlayerGuilds { get; set; }
        //public List<Comment> Comments { get; set; }
        //public List<User> Users { get; set; }
        public int PlayerID { get; set; }
        [DisplayName("Event Name")]
        public int currentPlayers { get; set; }

        [DisplayName("Guild Name")]
        public string LeaderName { get; set; }
    }
}
