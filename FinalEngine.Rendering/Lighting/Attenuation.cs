// <copyright file="Attenuation.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Lighting;

public sealed class Attenuation
{
    public Attenuation()
    {
        this.Constant = 1.0f;
        this.Linear = 0.07f;
        this.Quadratic = 0.017f;
    }

    public float Constant { get; }

    public float Linear { get; set; }

    public float Quadratic { get; set; }
}
