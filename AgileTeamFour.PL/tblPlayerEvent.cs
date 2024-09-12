using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblPlayerEvent
{
    public int PlayerEventID { get; set; }

    public int? PlayerID { get; set; }

    public int? EventID { get; set; }

    public string? Role { get; set; }

    public virtual tblEvent? Event { get; set; }

    public virtual tblPlayer? Player { get; set; }
}
