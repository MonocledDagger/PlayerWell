using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class PlayerGuild
    {
        public int PlayerGuildID { get; set; }
        public int GuildID { get; set; }
        public int PlayerID { get; set; }
        public string Role { get; set; }  // Leader, SubLeader, Member, etc.
        public DateTime JoinDate { get; set; }
    }
}
