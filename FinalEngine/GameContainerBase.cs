// <copyright file="GameContainerBase.cs" company="Software Antics">
// Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine;

using FinalEngine.Platform;
using Microsoft.Extensions.DependencyInjection;

//// TODO: Finish writing unit tests for this class.

public abstract class GameContainerBase : IDisposable
{
    protected GameContainerBase()
    {
        this.Window = ServiceLocator.Instance.GetRequiredService<IWindow>();
    }

    ~GameContainerBase()
    {
        this.Dispose(false);
    }

    protected bool IsDisposed { get; private set; }

    protected IWindow Window { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual void Draw()
    {
    }

    public virtual void Initialize()
    {
    }

    public virtual void LoadContent()
    {
    }

    public virtual void UnloadContent()
    {
    }

    public virtual void Update()
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        this.IsDisposed = true;
    }
}
