// <copyright file="SceneDataContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Contexts
{
    using System;
    using System.Drawing;
    using FinalEngine.Rendering;

    public class SceneDataContext : DataContextBase, ISceneDataContext
    {
        private readonly IRenderDevice renderDevice;

        public SceneDataContext(IRenderDevice renderDevice)
        {
            this.renderDevice = renderDevice ?? throw new ArgumentNullException(nameof(renderDevice), $"The specified {nameof(renderDevice)} parameter cannot be null.");
        }

        public void Initialize()
        {
            this.renderDevice.Initialize();
        }

        public void Render()
        {
            this.renderDevice.Clear(Color.CornflowerBlue);
        }
    }
}