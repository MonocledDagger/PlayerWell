using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblPostComment
{
    public int CommentID { get; set; }

    public int PostID { get; set; }

    public int AuthorID { get; set; }

    public string Text { get; set; } = null!;

    public DateTime TimePosted { get; set; }

    public int ParentCommentID { get; set; }
}
