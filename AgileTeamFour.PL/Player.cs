using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class Player
{
    public int PlayerID { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? IconPic { get; set; }

    public string? Bio { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<PlayerEvent> PlayerEvents { get; set; } = new List<PlayerEvent>();
}
