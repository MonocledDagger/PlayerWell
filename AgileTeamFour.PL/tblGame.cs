using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.PL
{
    public partial class tblGame
    {
        public int GameID { get; set; }
        public string GameName { get; set; }

        public string Platform { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Genre { get; set; }
    }
}
