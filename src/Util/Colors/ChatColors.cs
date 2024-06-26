﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Owop.Util;

/// <summary>Contains colors used in the OWOP chat interface.</summary>
public static class ChatColors
{
    /// <summary>The color of normal players' message headers.</summary>
    public static readonly Color Nickname = Color.FromArgb(58, 178, 255);

    /// <summary>The color of moderators' messages.</summary>
    public static readonly Color Moderator = Color.FromArgb(134, 255, 65);

    /// <summary>The color of admins' messages.</summary>
    public static readonly Color Admin = Color.FromArgb(255, 79, 79);

    /// <summary>The color of messages relayed from Discord.</summary>
    public static readonly Color Discord = Color.FromArgb(108, 255, 231);

    /// <summary>The color of server messages.</summary>
    public static readonly Color Server = Color.FromArgb(255, 65, 228);

    /// <summary>The color of private messages.</summary>
    public static readonly Color Tell = Color.FromArgb(255, 183, 53);
}
