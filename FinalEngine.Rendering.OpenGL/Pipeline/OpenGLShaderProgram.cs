﻿// <copyright file="OpenGLShaderProgram.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Rendering.OpenGL.Invocation;

    public class OpenGLShaderProgram : IOpenGLShaderProgram
    {
        private readonly IOpenGLInvoker invoker;

        private int id;

        public OpenGLShaderProgram(IOpenGLInvoker invoker, IEnumerable<IOpenGLShader> shaders)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");

            if (shaders == null)
            {
                throw new ArgumentNullException(nameof(shaders), $"The specified {nameof(shaders)} parameter cannot be null.");
            }

            this.id = this.invoker.CreateProgram();

            //// TODO: Should we make sure the user creates a program with both a vertex and fragment shader?
            //// TODO: How should we handle errors and warnings? Perhaps we need a way to decipher them and print out our own errors or warnings?
            //// TODO: I think the best way to handle this is to log information and say which framework has caused the error.
            //// TODO: Gradually, I can provide more context as to why the error has occurred.

            foreach (IOpenGLShader? shader in shaders)
            {
                if (shader == null)
                {
                    continue;
                }

                shader.Attach(this.id);
            }

            this.invoker.LinkProgram(this.id);
            this.invoker.ValidateProgram(this.id);

            string? log = this.invoker.GetProgramInfoLog(this.id);

            if (!string.IsNullOrWhiteSpace(log))
            {
                // TODO: Use appropriate logging system.
                throw new Exception(log);
            }
        }

        [ExcludeFromCodeCoverage]
        ~OpenGLShaderProgram()
        {
            this.Dispose(false);
        }

        protected bool IsDisposed { get; private set; }

        public void Bind()
        {
            this.invoker.UseProgram(this.id);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int GetUniformLocation(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), $"The specified {nameof(name)} parameter cannot be null, empty or contain only whitespace.");
            }

            return this.invoker.GetUniformLocation(this.id, name);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.id != -1)
                {
                    this.invoker.DeleteProgram(this.id);
                    this.id = -1;
                }
            }

            this.IsDisposed = true;
        }
    }
}