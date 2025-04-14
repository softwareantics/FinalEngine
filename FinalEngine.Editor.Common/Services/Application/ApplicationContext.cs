// <copyright file="ApplicationContext.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Common.Services.Application;

using System;
using System.IO.Abstractions;
using System.Reflection;
using FinalEngine.Editor.Common.Services.Environment;

internal sealed class ApplicationContext : IApplicationContext
{
    private readonly IEnvironmentContext environment;

    private readonly IFileSystem fileSystem;

    public ApplicationContext(IFileSystem fileSystem, IEnvironmentContext environment)
    {
        this.fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    public string DataDirectory
    {
        get
        {
            string directory = this.fileSystem.Path.Combine(this.environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Final Engine");

            if (!this.fileSystem.Directory.Exists(directory))
            {
                this.fileSystem.Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }

    public string Title
    {
        get { return $"Final Engine - {this.Version}"; }
    }

    public Version Version
    {
        get { return Assembly.GetExecutingAssembly().GetName().Version!; }
    }
}
