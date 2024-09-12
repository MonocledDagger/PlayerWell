using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblPlayer
{
    public int PlayerID { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public string? IconPic { get; set; }

    public string? Bio { get; set; }

    public virtual ICollection<tblComment> Comments { get; set; } = new List<tblComment>();

    public virtual ICollection<tblPlayerEvent> PlayerEvents { get; set; } = new List<tblPlayerEvent>();
}
