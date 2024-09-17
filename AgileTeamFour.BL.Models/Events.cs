using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
        public class Events
        {
            public int EventID { get; set; }
            public int GameID { get; set; }
            [DisplayName("Event Name")]
            public string EventName { get; set; }
            public string Server { get; set; }
            [DisplayName("Max Players")]
            public int MaxPlayers { get; set; }
            public string Type { get; set; }
            public string Platform { get; set; }
            public string Description { get; set; }
            [DisplayName("Event Time")]
            public DateTime DateTime { get; set; }
        }
}
