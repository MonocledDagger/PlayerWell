using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class PlayerEvent
    {
        public int PlayerEventID { get; set; }

        public int PlayerID { get; set; }

        public int EventID { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
