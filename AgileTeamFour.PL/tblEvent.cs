using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblEvent
{
    public int EventID { get; set; }

    public int GameID { get; set; }

    public string EventName { get; set; } = null!;

    public string? Server { get; set; }

    public int MaxPlayers { get; set; }

    public string? Type { get; set; }

    public string? Platform { get; set; }

    public string? Description { get; set; }

    public DateTime DateTime { get; set; }
}
