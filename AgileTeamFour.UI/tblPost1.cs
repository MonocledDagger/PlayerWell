﻿using System;
using System.Collections.Generic;

namespace AgileTeamFour.UI;

public partial class tblPost1
{
    public int PostID { get; set; }

    public DateTime TimePosted { get; set; }

    public int AuthorID { get; set; }

    public string? Image { get; set; }

    public string Text { get; set; } = null!;
}
