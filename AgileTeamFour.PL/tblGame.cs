using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblGame
{
    public int GameID { get; set; }

    public string GameName { get; set; } = null!;

    public string? Platform { get; set; }

    public string Description { get; set; } = null!;

    public string? Picture { get; set; }

    public string? Genre { get; set; }
}
