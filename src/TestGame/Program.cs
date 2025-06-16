namespace TestGame;

using FinalEngine;
using FinalEngine.Audio.Extensions;
using FinalEngine.Extensions;
using FinalEngine.Platform.Extensions;
using FinalEngine.Rendering.Extensions;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main()
    {
        var services = new ServiceCollection()
            .AddFinalEngine(x =>
            {
                x.UseCasl();
                x.UseOpenGL();
                x.UseOpenTK();
            });

        var provider = services.BuildServiceProvider();

        using (var driver = provider.GetRequiredService<IEngineDriver>())
        {
            driver.Drive();
        }
    }
}
