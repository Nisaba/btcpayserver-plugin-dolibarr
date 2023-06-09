using System;
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


    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var settings = (await _SettingsRepository.GetSettingAsync<DolibarrSettings>()) ?? new DolibarrSettings();
        return View(settings);
    }

    [HttpPost]
    public async Task<IActionResult> Index(DolibarrSettings model, string command)
    {
        if (ModelState.IsValid)
        {
            switch (command)
            {
                case "Test":
                    try
                    {
                    }
                    catch (Exception e)
                    {
                        TempData[WellKnownTempData.ErrorMessage] = $"Dolibarr access error : {e.Message}";
                    }
                    break;
                case "Save":
                    await _SettingsRepository.UpdateSetting(model);
                    TempData[WellKnownTempData.SuccessMessage] = "Log settings saved.";
                    break;
                default:
                    break;
            }
        } else
        {
            TempData[WellKnownTempData.ErrorMessage] = "Data are not valid";
        }
        return View("Index", model);
    }

}
