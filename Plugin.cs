using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Models;
using BTCPayServer.Abstractions.Services;
using BTCPayServer.Plugins.Dolibarr.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BTCPayServer.Plugins.Dolibarr;

public class Plugin : BaseBTCPayServerPlugin
{
    public override IBTCPayServerPlugin.PluginDependency[] Dependencies { get; } = new[]
    {
        new IBTCPayServerPlugin.PluginDependency { Identifier = nameof(BTCPayServer), Condition = ">=1.8.2" }
    };

    public override void Execute(IServiceCollection services)
    {
        services.AddSingleton<IUIExtension>(new UIExtension("DolibarrPluginHeaderNav", "header-nav"));
        services.AddHostedService<ApplicationPartsLogger>();
        services.AddHostedService<PluginMigrationRunner>();
        services.AddSingleton<DolibarrService>();
        services.AddSingleton<DolibarrDbContextFactory>();
        services.AddDbContext<DolibarrDbContext>((provider, o) =>
        {
            DolibarrDbContextFactory factory = provider.GetRequiredService<DolibarrDbContextFactory>();
            factory.ConfigureBuilder(o);
        });
    }
}
