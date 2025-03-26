using Microsoft.AspNetCore.Mvc;
using TaskManagment.Core.Entities;
using TaskManagment.Infrastructure.Data;

namespace TaskManagment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    public TaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("getTasks")]
    public async Task<IActionResult> GetTasks()
    {

        var tasks = _context.WorkTasks;
        return Ok(tasks);
    }
}
