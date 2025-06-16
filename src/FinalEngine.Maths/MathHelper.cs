// <copyright file="MathHelper.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Maths;

using System;

public static class MathHelper
{
    public static float DegreesToRadians(float angle)
    {
        return (float)Math.PI / 180.0f * angle;
    }

    public static float RadiansToDegrees(float angle)
    {
        return 180.0f / (float)Math.PI * angle;
    }
}
