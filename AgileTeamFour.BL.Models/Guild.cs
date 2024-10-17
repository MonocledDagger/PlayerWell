using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Guild
    {
        public int GuildId { get; set; }
        public string GuildName { get; set;}

        public string Description { get; set; }

        public int LeaderId {  get; set; }
    }
}
