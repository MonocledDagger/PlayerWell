using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblFriend
{
    public int ID { get; set; }

    public string Status { get; set; } = null!;

    public int SenderID { get; set; }

    public int ReceiverID { get; set; }
}
