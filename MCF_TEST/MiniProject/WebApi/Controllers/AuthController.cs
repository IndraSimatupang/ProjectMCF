using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModel;
using WebApi.Repositories;
using WebApi.Security;

namespace WebApi.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _context.MsUsers
.Where(u => u.UserName == request.UserName && u.Password == request.Password && u.IsActive)
.FirstOrDefaultAsync();

        if (user == null)
            return Unauthorized();

        return Ok(new { UserId = user.UserId, UserName = user.UserName });
    }
}

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
}
