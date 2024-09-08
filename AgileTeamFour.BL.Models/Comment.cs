using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Comment
    {
        //Primary key
        public int CommentID { get; set; }

        //Foreign Key references an EventID, the primary key of the event table
        public int EventID { get; set; }

        public DateTime TimePosted { get; set; }

        //Foreign Key references a PlayerID, the primary key in the player Table
        public int AuthorID { get; set; }

        public string AuthorName { get; set; }
        public string Text { get; set; }

    }
}
