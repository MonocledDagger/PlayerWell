using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Game
    {
        public int GameID { get; set; }
        [DisplayName("Game Name")]
        public string GameName { get; set; }

        public string Platform { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Genre { get; set; }
    }
}
