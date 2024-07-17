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
    public class AuthController : Controller
{
    private readonly HttpClient _httpClient;

    public AuthController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://localhost:5001/api/auth/login", content);

        if (!response.IsSuccessStatusCode)
            return View();

        var responseBody = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<UserResponse>(responseBody);

        HttpContext.Session.SetString("UserId", user.UserId.ToString());
        HttpContext.Session.SetString("UserName", user.UserName);

        return RedirectToAction("Index", "Home");
    }
}

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class UserResponse
{
    public long UserId { get; set; }
    public string UserName { get; set; }
}
}
