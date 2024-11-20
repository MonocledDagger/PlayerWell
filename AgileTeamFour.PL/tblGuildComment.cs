using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblGuildComment
{
    public int CommentID { get; set; }

    public int GuildId { get; set; }

    public DateTime TimePosted { get; set; }

    public int AuthorID { get; set; }

    public string Text { get; set; } = null!;
}
