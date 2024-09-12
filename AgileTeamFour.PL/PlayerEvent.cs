using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class PlayerEvent
{
    public int PlayerEventID { get; set; }

    public int? PlayerID { get; set; }

    public int? EventID { get; set; }

    public string? Role { get; set; }

    public virtual Event? Event { get; set; }

    public virtual Player? Player { get; set; }
}
