﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
        public class Events
        {
            public int EventID { get; set; }
            [DisplayName("Game Name")]
            public int GameID { get; set; }
            [DisplayName("Event Name")]
            public string EventName { get; set; }
            public string Server { get; set; }
            [DisplayName("Max Players")]
            [Range(1, int.MaxValue, ErrorMessage = "Max Players must be a positive number")]
            public int MaxPlayers { get; set; }
            
            public string Type { get; set; }
            public string Platform { get; set; }
            public string Description { get; set; }
            [DisplayFormat(DataFormatString = "{0:MMM dd yyyy @ hh:mm tt}")]
            [DisplayName("Event Time")]
            public DateTime DateTime { get; set; }

            public int AuthorId { get; set; }
            public List<Game> Games { get; set; } = new List<Game>();

        [DisplayName("Guild Name")]
        public int? GuildId {  get; set; }
    }
}
