using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModel;
using WebApp.Security;
using WebApp.Services;

namespace WebApp.Controllers
{
   public class BpkbController : Controller
{
    private readonly HttpClient _httpClient;

    public BpkbController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var response = await _httpClient.GetAsync("https://localhost:5001/api/bpkb/storage-locations");
        var responseBody = await response.Content.ReadAsStringAsync();
        var locations = JsonConvert.DeserializeObject<List<StorageLocation>>(responseBody);

        var model = new BpkbViewModel { StorageLocations = locations };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BpkbViewModel model)
    {
        var content = new StringContent(JsonConvert.SerializeObject(model.Bpkb), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://localhost:5001/api/bpkb", content);

        if (!response.IsSuccessStatusCode)
            return View(model);

        return RedirectToAction("Index", "Home");
    }
}

public class StorageLocation
{
    public string LocationId { get; set; }
    public string LocationName { get; set; }
}

public class BpkbViewModel
{
    public TrBpkb Bpkb { get; set; }
    public List<StorageLocation> StorageLocations { get; set; }
}
}
