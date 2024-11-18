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
        public string Text { get; set; }
        public string Image { get; set; }
        public int AuthorID { get; set; }
        public string UserName { get; set; }
        public string IconPic { get; set; }
        public string Bio { get; set; }
        public double? AverageStarsOutOf5 { get; set; }
        public string ReviewSummary { get; set; }
        public List<PostComment> Comments { get; set; } // Ensure this is List<PostComment>
    }
}
