// <copyright file="MouseButton.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Input.Mouses;

using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute", Justification = "Not required by API")]
public enum MouseButton
{
    Unknown = -1,

    Button1 = 0,

    Left = Button1,

    Button2 = 1,

    Right = Button2,

    Button3 = 2,

    Middle = Button3,

    Button4 = 3,

    Button5 = 4,

    Button6 = 5,

    Button7 = 6,

    Button8 = 7,

    Last = Button8,
}
