using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagment.Core.Entities.Auth;
using TaskManagment.Infrastructure.Data;

namespace TaskManagment.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("getProjects")]
    public async Task<IActionResult> GetProject()
    {
        var projects = _context.Projects;

        return Ok(projects);
    }
}
