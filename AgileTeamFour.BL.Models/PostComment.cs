using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileTeamFour.BL.Models
{
    public class PostComment
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int AuthorID { get; set; }
        public DateTime TimePosted { get; set; }
        public string Text { get; set; }
        public int? ParentCommentID { get; set; }
        public List<PostComment> Replies { get; set; } = new List<PostComment>();
        public PostComment Comments { get; set; }
       
    }


}
