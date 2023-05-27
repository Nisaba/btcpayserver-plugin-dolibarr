using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace BTCPayServer.Plugins.Dolibarr;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DolibarrDbContext>
{
    public DolibarrDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<DolibarrDbContext> builder = new DbContextOptionsBuilder<DolibarrDbContext>();

        builder.UseSqlite("Data Source=temp.db");

        return new DolibarrDbContext(builder.Options, true);
    }
}

public class DolibarrDbContextFactory : BaseDbContextFactory<DolibarrDbContext>
{
    public DolibarrDbContextFactory(IOptions<DatabaseOptions> options) : base(options, "BTCPayServer.Plugins.Dolibarr")
    {
    }

    public override DolibarrDbContext CreateContext()
    {
        DbContextOptionsBuilder<DolibarrDbContext> builder = new DbContextOptionsBuilder<DolibarrDbContext>();
        ConfigureBuilder(builder);
        return new DolibarrDbContext(builder.Options);
    }
}
