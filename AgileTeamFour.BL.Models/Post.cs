using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public DateTime TimePosted { get; set; }
        public int AuthorID { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
