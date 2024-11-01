using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblFriendComment
{
    public int ID { get; set; }
    public int FriendSentToID { get; set; }
    public DateTime TimePosted { get; set; }
    public int AuthorID { get; set; }
    public string Text { get; set; }
}
