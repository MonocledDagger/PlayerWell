using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Guild
    {
        [DisplayName("Guild ID Number")]
        public int GuildId { get; set; }
        [DisplayName("Guild Name")]
        public string GuildName { get; set;}

        public string Description { get; set; }

        public int LeaderId {  get; set; }

        //public List<User> Members { get; set; }
    }
}
