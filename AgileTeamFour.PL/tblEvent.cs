using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblEvent
{
    public int EventID { get; set; }

    public int? GameID { get; set; }

    public string EventName { get; set; } = null!;

    public string? Server { get; set; }

    public int? MaxPlayers { get; set; }

    public string? EventType { get; set; }

    public string? Platform { get; set; }

    public string? Description { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual ICollection<tblComment> Comments { get; set; } = new List<tblComment>();

    public virtual tblGame? Game { get; set; }

    public virtual ICollection<tblPlayerEvent> PlayerEvents { get; set; } = new List<tblPlayerEvent>();
}
