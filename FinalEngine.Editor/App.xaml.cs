namespace FinalEngine.Editor
{
    using System;
    using System.Windows;
    using FinalEngine.Editor.Contexts;
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
        private readonly IServiceProvider provider;

        public App()
        {
            this.provider = this.CreateAndConfigureServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.provider.GetRequiredService<MainView>().Show();
        }

        private IServiceProvider CreateAndConfigureServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IOpenGLInvoker, OpenGLInvoker>();
            services.AddSingleton<IRenderDevice, OpenGLRenderDevice>();

            services.AddSingleton<IMainDataContext, MainDataContext>();
            services.AddSingleton<ISceneDataContext, SceneDataContext>();

            services.AddSingleton<MainView>();

            return services.BuildServiceProvider();
        }
    }
}