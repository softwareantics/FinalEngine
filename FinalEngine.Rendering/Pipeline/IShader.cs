﻿namespace FinalEngine.Rendering.Pipeline
{
    using System;

    public interface IShader : IDisposable
    {
        PipelineTarget EntryPoint { get; }
    }
}