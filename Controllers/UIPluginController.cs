using System.Collections.Generic;
using System.Threading.Tasks;
using BTCPayServer.Abstractions.Constants;
using BTCPayServer.Abstractions.Contracts;
using BTCPayServer.Client;
using BTCPayServer.Plugins.Dolibarr.Data;
using BTCPayServer.Plugins.Dolibarr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BTCPayServer.Plugins.Dolibarr;

[Route("~/plugins/dolibarr")]
[Authorize(AuthenticationSchemes = AuthenticationSchemes.Cookie, Policy = Policies.CanViewProfile)]
public class UIPluginController : Controller
{
    private readonly ISettingsRepository _SettingsRepository;
    private readonly DolibarrService _PluginService;

    public UIPluginController(DolibarrService PluginService, ISettingsRepository settingsRepository)
    {
        _PluginService = PluginService;
        _SettingsRepository = settingsRepository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(new PluginPageViewModel { Data = await _PluginService.Get() });
    }
}

public class PluginPageViewModel
{
    public List<PluginData> Data { get; set; }
}
