using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Plugins.Dolibarr.Data;
using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.EntityFrameworkCore;

namespace BTCPayServer.Plugins.Dolibarr.Services;

public class DolibarrService
{
    private readonly ISettingsRepository _SettingsRepository;
    private readonly DolibarrDbContextFactory _PluginDbContextFactory;

    private const string UrlSuffix = "api/index.php/";
    private HttpClient client;

    public DolibarrService()
    {

    }

    public DolibarrService(ISettingsRepository settingsRepository, DolibarrDbContextFactory PluginDbContextFactory)
    {
        _SettingsRepository = settingsRepository;
        _PluginDbContextFactory = PluginDbContextFactory;
    }

    // https://code-maze.com/fetching-data-with-httpclient-in-aspnetcore/
    public async Task<String> DoTest(DolibarrSettings settings)
    {
        InitHttpClient(settings);

        try
        {
            var response = await client.GetAsync("invoices");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return "OK";
            } else
            {
                return response.StatusCode.ToString() + " - " + response.ReasonPhrase;
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    private void InitHttpClient(DolibarrSettings mySettings)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(mySettings.Url + UrlSuffix);
        client.DefaultRequestHeaders.Add("DOLAPIKEY", mySettings.Token);
        client.Timeout = new TimeSpan(0, 0, 30);
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

