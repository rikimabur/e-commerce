using System.Reflection;

namespace BuildingBlocks.API.Modules;
public static class ModuleDiscovery
{
    public static IReadOnlyCollection<IModule> DiscoverModules()
    {
        return [.. AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types.Where(t => t != null)!;
                }
            })
            .Where(t =>
                typeof(IModule).IsAssignableFrom(t) &&
                t is { IsAbstract: false, IsInterface: false })
            .Select(Activator.CreateInstance)
            .Cast<IModule>()];
    }
}
