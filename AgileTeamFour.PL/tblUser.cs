using System;
using System.Collections.Generic;

namespace AgileTeamFour.PL;

public partial class tblUser
{
    public int UserID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
}
