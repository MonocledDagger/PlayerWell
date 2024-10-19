using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblPost
{
    public int PostID { get; set; }

    public DateTime TimePosted { get; set; }

    public int AuthorID { get; set; }

    public string? Image { get; set; }

    public string Text { get; set; } = null!;
}
