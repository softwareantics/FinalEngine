// <copyright file="App.xaml.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Editor.Desktop;

using System.Diagnostics;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using FinalEngine.Audio.OpenAL.Extensions;
using FinalEngine.ECS.Extensions;
using FinalEngine.Editor.Common.Extensions;
using FinalEngine.Editor.Desktop.Extensions;
using FinalEngine.Editor.Desktop.Services.Actions;
using FinalEngine.Editor.Desktop.Services.Layout;
using FinalEngine.Editor.Desktop.Views;
using FinalEngine.Editor.Desktop.Views.Dialogs.Entities;
using FinalEngine.Editor.Desktop.Views.Dialogs.Layout;
using FinalEngine.Editor.ViewModels;
using FinalEngine.Editor.ViewModels.Dialogs.Entities;
using FinalEngine.Editor.ViewModels.Dialogs.Layout;
using FinalEngine.Editor.ViewModels.Docking;
using FinalEngine.Editor.ViewModels.Inspectors;
using FinalEngine.Editor.ViewModels.Projects;
using FinalEngine.Editor.ViewModels.Scenes;
using FinalEngine.Editor.ViewModels.Services;
using FinalEngine.Editor.ViewModels.Services.Actions;
using FinalEngine.Editor.ViewModels.Services.Entities;
using FinalEngine.Editor.ViewModels.Services.Interactions;
using FinalEngine.Editor.ViewModels.Services.Layout;
using FinalEngine.Input.Extensions;
using FinalEngine.Physics.Extensions;
using FinalEngine.Rendering.Extensions;
using FinalEngine.Rendering.OpenGL.Extensions;
using FinalEngine.Resources.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public partial class App : Application
{
    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .Build();
    }

    private static IHost? AppHost { get; set; }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync().ConfigureAwait(false);
        base.OnExit(e);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync().ConfigureAwait(false);

        var viewModel = AppHost.Services.GetRequiredService<IMainViewModel>();

        var view = new MainView()
        {
            DataContext = viewModel,
        };

        view.ShowDialog();

        base.OnStartup(e);
    }

    private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.AddConsole().SetMinimumLevel(Debugger.IsAttached ? LogLevel.Debug : LogLevel.Information);
        });

        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

        services.AddECS();
        services.AddOpenGL();
        services.AddOpenAL();
        services.AddInput();
        services.AddPhysics();
        services.AddRendering();
        services.AddResourceManager();
        services.AddEditorPlatform();

        services.AddCommon();

        services.AddFactory<IProjectExplorerToolViewModel, ProjectExplorerToolViewModel>();
        services.AddFactory<ISceneHierarchyToolViewModel, SceneHierarchyToolViewModel>();
        services.AddFactory<IPropertiesToolViewModel, PropertiesToolViewModel>();
        services.AddFactory<IConsoleToolViewModel, ConsoleToolViewModel>();
        services.AddFactory<IEntitySystemsToolViewModel, EntitySystemsToolViewModel>();
        services.AddFactory<ISceneViewPaneViewModel, SceneViewPaneViewModel>();
        services.AddFactory<IDockViewModel, DockViewModel>();
        services.AddFactory<ISaveWindowLayoutViewModel, SaveWindowLayoutViewModel>();
        services.AddFactory<IManageWindowLayoutsViewModel, ManageWindowLayoutsViewModel>();
        services.AddFactory<ICreateEntityViewModel, CreateEntityViewModel>();
        services.AddSingleton<IMainViewModel, MainViewModel>();

        services.AddTransient<IViewable<ISaveWindowLayoutViewModel>, SaveWindowLayoutView>();
        services.AddTransient<IViewable<IManageWindowLayoutsViewModel>, ManageWindowLayoutsView>();
        services.AddTransient<IViewable<ICreateEntityViewModel>, CreateEntityView>();

        services.AddSingleton<IEntityComponentTypeResolver, EntityComponentTypeResolver>();

        services.AddSingleton<IUserActionRequester, UserActionRequester>();
        services.AddSingleton<ILayoutManager, LayoutManager>();

        services.AddSingleton<IViewPresenter>(x =>
        {
            return new ViewPresenter(x.GetRequiredService<ILogger<ViewPresenter>>(), x);
        });
    }
}
