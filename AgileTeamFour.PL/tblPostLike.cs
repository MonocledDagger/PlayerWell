using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblPostLike
{
    public int LikeID { get; set; }

    public int PostID { get; set; }

    public int UserID { get; set; }

    public DateTime? LikeDate { get; set; }
}
