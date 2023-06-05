using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BTCPayServer.Plugins.Dolibarr.Data;
using Microsoft.EntityFrameworkCore;

namespace BTCPayServer.Plugins.Dolibarr.Services;

public class DolibarrService
{
    private readonly DolibarrDbContextFactory _PluginDbContextFactory;

    public DolibarrService(DolibarrDbContextFactory PluginDbContextFactory)
    {
        _PluginDbContextFactory = PluginDbContextFactory;
    }

    public async Task SendInvoice()
    {
        await using var context = _PluginDbContextFactory.CreateContext();

        await context.PluginRecords.AddAsync(new DolibarrData { Timestamp = DateTimeOffset.UtcNow });
        await context.SaveChangesAsync();
    }

    public async Task<List<DolibarrData>> Get()
    {
        await using var context = _PluginDbContextFactory.CreateContext();

        return await context.PluginRecords.ToListAsync();
    }
}

