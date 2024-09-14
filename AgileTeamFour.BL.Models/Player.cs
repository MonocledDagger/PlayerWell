using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Player
    {
        //Primary key
        public int PlayerID { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //This string would be an image path
        public string IconPic { get; set; }

        public string Bio { get; set; }

        public DateTime DateTime { get; set; }
    }
}
