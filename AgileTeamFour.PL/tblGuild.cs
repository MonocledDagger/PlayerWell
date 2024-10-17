using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblGuild
{
    public int GuildId { get; set; }

    public string GuildName { get; set; } = null!;

    public string? Description { get; set; }

    public int LeaderId { get; set; }
}
