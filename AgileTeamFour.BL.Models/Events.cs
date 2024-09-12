﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
        public class Events
        {
            public int EventID { get; set; }
            public int? GameID { get; set; }
            public string EventName { get; set; }
            public string Server { get; set; }

            public int? MaxPlayers { get; set; }

            public string EventType { get; set; }

            public string Platform { get; set; }
            public string Description { get; set; }

            public DateTime? DateTime { get; set; }
        }
}
