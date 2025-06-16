namespace FinalEngine.Hosting;

using Microsoft.Extensions.DependencyInjection;

public interface IEngineBuilder
{
    IServiceCollection Services { get; }
}
