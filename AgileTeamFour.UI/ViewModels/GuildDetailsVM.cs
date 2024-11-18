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
        public List<GuildComment> GuildComments { get; set; }
        //public List<User> Users { get; set; }
        public int PlayerID { get; set; }
        [DisplayName("Current Members")]
        public int currentPlayers { get; set; }

        [DisplayName("Leader Name")]
        public string LeaderName { get; set; }

        //Load Events with GuildId
        public IEnumerable<Events> Events { get; set; }
    }
}
