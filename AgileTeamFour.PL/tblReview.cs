using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblReview
{
    public int ReviewID { get; set; }

    public int StarsOutOf5 { get; set; }

    public string? ReviewText { get; set; }

    public int AuthorID { get; set; }

    public int RecipientID { get; set; }

    public DateTime DateTime { get; set; }
}
