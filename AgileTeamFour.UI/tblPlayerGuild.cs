using System;
using System.Collections.Generic;

namespace AgileTeamFour.UI;

public partial class tblPlayerGuild
{
    public int PlayerGuildID { get; set; }

    public int PlayerID { get; set; }

    public int GuildID { get; set; }

    public string? Role { get; set; }

    public DateTime? JoinDate { get; set; }
}
