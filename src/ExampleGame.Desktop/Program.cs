namespace ExampleGame.Desktop;

using FinalEngine.Audio.Extensions;
using FinalEngine.Hosting;
using FinalEngine.Hosting.Extensions;
using FinalEngine.Platform.Extensions;
using FinalEngine.Rendering.Extensions;
using Microsoft.Extensions.DependencyInjection;

internal static class Program
{
    private static void Main()
    {
        var services = new ServiceCollection()
            .AddFinalEngine(x =>
            {
                x.UseOpenTK();
                x.UseOpenGL();
                x.UseCasl();
            });

        var provider = services.BuildServiceProvider();

        using (var driver = provider.GetRequiredService<IEngineDriver>())
        {
            driver.Start();
        }
    }
}
