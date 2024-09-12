using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class Event
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

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Game? Game { get; set; }

    public virtual ICollection<PlayerEvent> PlayerEvents { get; set; } = new List<PlayerEvent>();
}
