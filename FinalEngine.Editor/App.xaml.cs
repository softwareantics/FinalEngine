// <copyright file="App.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor
{
    using System;
    using System.Windows;
    using FinalEngine.Editor.ViewModels;
    using FinalEngine.Editor.Views;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///   Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///   The service provider.
        /// </summary>
        private readonly IServiceProvider provider;

        /// <summary>
        ///   Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.provider = CreateAndConfigureServiceProvider();
        }

        /// <summary>
        ///   Raises the <see cref="Application.Startup"/> event.
        /// </summary>
        /// <param name="e">
        ///   A <see cref="StartupEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            this.provider.GetRequiredService<MainView>().Show();
        }

        /// <summary>
        ///   Creates and configures the service provider.
        /// </summary>
        /// <returns>
        ///   The newly created and configured service provider.
        /// </returns>
        private static IServiceProvider CreateAndConfigureServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
            services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

            services.AddSingleton<ISceneViewModel, SceneViewModel>();
            services.AddSingleton<IPropertiesViewModel, PropertiesViewModel>();
            services.AddSingleton<ISceneHierarchyViewModel, SceneHierarchyViewModel>();
            services.AddSingleton<IProjectExplorerViewModel, ProjectExplorerViewModel>();
            services.AddSingleton<IConsoleViewModel, ConsoleViewModel>();
            services.AddSingleton<IDockViewModel, DockViewModel>();
            services.AddSingleton<IMainMenuViewModel, MainMenuViewModel>();
            services.AddSingleton<IMainViewModel, MainViewModel>();

            services.AddSingleton<MainView>();

            return services.BuildServiceProvider();
        }
    }
}