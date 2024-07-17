using System.Threading.Tasks;
using WebApi.DataModels;
using System.Collections.Generic;
using System.Linq;
using ViewModel;
using WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApi.Security;

namespace WebApi.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class BpkbController : ControllerBase
{
    private readonly AppDbContext _context;

    public BpkbController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("storage-locations")]
    public async Task<IActionResult> GetStorageLocations()
    {
        var locations = await _context.MsStorageLocations.ToListAsync();
        return Ok(locations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBpkb([FromBody] TrBpkb bpkb)
    {
        _context.TrBpkbs.Add(bpkb);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBpkbById), new { id = bpkb.AgreementNumber }, bpkb);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBpkbById(string id)
    {
        var bpkb = await _context.TrBpkbs.FindAsync(id);

        if (bpkb == null)
            return NotFound();

        return Ok(bpkb);
    }
}
}
