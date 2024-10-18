using System;
using System.Collections.Generic;

namespace AgileTeamFour.UI;

public partial class tblComment
{
    public int CommentID { get; set; }

    public int EventID { get; set; }

    public DateTime TimePosted { get; set; }

    public int AuthorID { get; set; }

    public string Text { get; set; } = null!;
}
