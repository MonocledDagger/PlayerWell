using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class Comment
{
    public int CommentID { get; set; }

    public int? EventID { get; set; }

    public DateTime? TimePosted { get; set; }

    public int? AuthorID { get; set; }

    public string? AuthorName { get; set; }

    public string? Text { get; set; }

    public virtual Player? Author { get; set; }

    public virtual Event? Event { get; set; }
}
