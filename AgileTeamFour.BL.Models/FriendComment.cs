using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AgileTeamFour.BL.Models
{
    public class FriendComment
    {

        public int ID { get; set; }
        public int FriendSentToID { get; set; }
        public DateTime TimePosted { get; set; }
        public int AuthorID { get; set; }
        public string Text { get; set; }
    }
}
